using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles;
using SRM.Domain.Shuttles.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public class ShuttleAdviceService : IShuttleAdviceService
    {
        private readonly IRepository<ShuttleStudentOperasionLessonRelation> _studentLessonRelationRepository;
        private readonly IRepository<ShuttleOperation> _shuttleOperationRepo;
        private readonly IRepository<ShuttleStudentOperation> _studentOperationRepo;
        private readonly IRepository<Student> _studentRepo;

        private readonly IRepository<StudentAddress> _studentAdress;
        private readonly IRepository<StudentAvailableTime> _availavleTime;

        public readonly IRepository<Location> _location;
        public ShuttleAdviceService(
            IRepository<ShuttleStudentOperasionLessonRelation> studentLessonRelationRepository,
            IRepository<ShuttleOperation> shuttleOperationRepo,
            IRepository<Student> studentRepo,
            IRepository<StudentAddress> studentAdress,
            IRepository<Location> location,
            IRepository<StudentAvailableTime> availavleTime,
            IRepository<ShuttleStudentOperation> studentOperationRepo
            )
        {
            _studentLessonRelationRepository = studentLessonRelationRepository;
            _shuttleOperationRepo = shuttleOperationRepo;
            _studentRepo = studentRepo;
            _studentAdress = studentAdress;
            _location = location;
            _availavleTime = availavleTime;
            _studentOperationRepo = studentOperationRepo;

        }

        /// <summary>
        /// Öğrenci önerileri
        /// </summary>       
        /// <returns></returns>
        public GetAdviceResult GetAdvices(GetAdviceRequest request)
        {
            long shuttleOpationId = request.ShuttleOperationId;//servis operasyon id

            var currentSOperation = _shuttleOperationRepo.Table
                                 .Include(x => x.ShuttleStudentOperations)
                                 .ThenInclude(x => x.Student)
                                 .FirstOrDefault(x => x.Id == shuttleOpationId);

            DateTime adviceDay = currentSOperation != null ? currentSOperation.DateTime : DateTime.Now; //önerinin hangi güne göre hesaplancagı

            //Mevcut opeasyon datasında ogrenci kısıtları kontrol edilmeli
            DateTime endDate = adviceDay.Date;// new DateTime(2019, 4, 1);// DateTime.Now;
            DateTime startDate = endDate.AddDays(-90);//Telafinin hesaplanmaya başlancagı tarih

            //Hesaplama datası operasyon durumu bitmiş servisler üzerinden yapılmaktadır.
            var adress = GetStudentByLocation(request.MapsCorners);

            var advicesX = _studentLessonRelationRepository.Table
                .Where(en =>
                       en.ShuttleStudentOperation.ShuttleOperation.DateTime.Date >= startDate.Date
                    && en.ShuttleStudentOperation.ShuttleOperation.DateTime.Date <= endDate.Date
                    && en.ShuttleStudentOperation.Student.IsActive//aktif öğrenciler
                    && en.ShuttleStudentOperation.ShuttleOperation.ShuttleOperationStatus == ShuttleOperationStatus.OprationFinished
                    //Biten operasyonlar
                    );

            if (shuttleOpationId != 0)
            {
                //Operasyonda olan ogrencileri dahil etmiyoruz
                advicesX = advicesX.Where(en => !currentSOperation.ShuttleStudentOperations.Any(x => x.Student.Id == en.ShuttleStudentOperation.Student.Id));
            }

            var advices = advicesX.GroupBy(en => new { en.ShuttleStudentOperation.Student, Mount = en.ShuttleStudentOperation.ShuttleOperation.DateTime.Month })
                .Select(en => new
                {
                    StudentId = en.Key.Student.Id,
                    // DisContinuityCount = en.Sum(u => u.PlannedLessonCount - u.CompletedLessonCount),//Hesaplama süresi boyunca katılmadıgı ders sayısı
                    MounthlyDiscontinuityCount = en.Sum(u => u.PlannedLessonCount - u.CompletedLessonCount),
                    en.Key.Mount
                    //  en.Where(x => x.ShuttleStudentOperation.ShuttleOperation.DateTime.Year == adviceDay.Year
                    //      && x.ShuttleStudentOperation.ShuttleOperation.DateTime.Month == adviceDay.Month)
                    // .Sum(u => u.PlannedLessonCount - u.CompletedLessonCount),//Bu ay eksik katılmadıgı ders sayısı

                });

            var addVice = advices.GroupBy(x => x.StudentId).Select(
                     x => new
                     {
                         StudentId = x.Key,
                         DisContinuityCount = x.Sum(u => u.MounthlyDiscontinuityCount),
                         MounthlyDiscontinuityCount = x.Sum(u => u.Mount == endDate.Month ? u.MounthlyDiscontinuityCount : 0)
                     });

            var res1 = (from advice in addVice
                        join adres in adress on advice.StudentId equals adres.StudentId
                        select new
                        {
                            advice.StudentId,
                            advice.DisContinuityCount,
                            advice.MounthlyDiscontinuityCount,
                            adres.Name,
                            adres.Surname,
                            adres.LocationX,
                            adres.LocationY
                        }).OrderByDescending(x => x.DisContinuityCount);
            var totalCount = res1.Count();//Sonradan eklendi performansa etksi olursa kaldıralım

            var res = res1.Skip(request.PageNumber * request.PageSize).Take(request.PageSize).ToList();
            var times = GetStudentAvaiblableTimes(adviceDay);

            var resultWithTime = (from student in res
                                  join time in times on student.StudentId equals time.Student.Id into studentTimes
                                  from time in studentTimes.DefaultIfEmpty()
                                  select new
                                  {
                                      student,
                                      time
                                  }
                                    ).GroupBy(x => x.student).Select(x => new
                                    {
                                        x.Key.StudentId,
                                        x.Key.DisContinuityCount,
                                        x.Key.MounthlyDiscontinuityCount,
                                        x.Key.Name,
                                        x.Key.Surname,
                                        x.Key.LocationX,
                                        x.Key.LocationY,
                                        IsAvaible = !x.Any(y => y.time != null && !y.time.IsAvaible)
                                    }).ToList();

            return new GetAdviceResult
            {
                Advices = resultWithTime,
                TotalCount = totalCount
            };

            //  return resultWithTime;
        }

        public IQueryable<StudentAdress> GetStudentByLocation(List<MapsCorner> mapsCorners)
        {

            // var l1 = new LocationPoint { LocationX = 41.076650, LocationY = 28.9687938 };
            // var l2 = new LocationPoint { LocationX = 41.075675, LocationY = 29.012991 };
            // var l3 = new LocationPoint { LocationX = 41.097186, LocationY = 29.011714 };
            // var l4 = new LocationPoint { LocationX = 41.097513, LocationY = 28.966428 };
            double minx1 = 0, minx2 = 0, maxx1 = 0, maxx2 = 0, miny1 = 0, miny2 = 0, maxy1 = 0, maxy2 = 0;
            bool checkCorner = true;

            if (mapsCorners != null)
            {
                if (mapsCorners.Count == 4)
                {
                    var locx = mapsCorners.OrderBy(x => x.LocationX);

                    minx1 = locx.Take(1).First().LocationX;
                    minx2 = locx.Skip(1).Take(1).First().LocationX;
                    maxx1 = locx.Skip(2).Take(1).First().LocationX;
                    maxx2 = locx.Skip(3).Take(1).First().LocationX;

                    var locy = mapsCorners.OrderBy(x => x.LocationY);
                    miny1 = locy.Take(1).First().LocationY;
                    miny2 = locy.Skip(1).Take(1).First().LocationY;
                    maxy1 = locy.Skip(2).Take(1).First().LocationY;
                    maxy2 = locy.Skip(3).Take(1).First().LocationY;

                    checkCorner = false;
                }
            }
            var adress = _studentAdress.Table.Include(x => x.Student)
                       .Include(x => x.Address.Location)
                       .Where(x => x.Address.Location != null)
                       .Where(x =>
                      checkCorner || (
                        (x.Address.Location.LocationX > minx1
                       || x.Address.Location.LocationX > minx2)
                       && (x.Address.Location.LocationX < maxx1
                       || x.Address.Location.LocationX < maxx2)

                       && (x.Address.Location.LocationY > miny1
                       || x.Address.Location.LocationY > miny2)
                       && (x.Address.Location.LocationY < maxy1
                       || x.Address.Location.LocationY < maxy2)

                    ))
                       .Select(x => new StudentAdress
                       {
                           StudentId = x.Student.Id,
                           Name = x.Student.Name,
                           Surname = x.Student.Surname,
                           LocationX = x.Address.Location.LocationX,
                           LocationY = x.Address.Location.LocationY
                       });
            return adress;
        }

        public IQueryable<StudentAvailableTime> GetStudentAvaiblableTimes(DateTime adviceDay)
        {
            var availableTimes = _availavleTime.Table.Include(x => x.Student)
                       .Include(x => x.IncludedDate).
                           Where(x =>
                           //x => x.Student.Id == studentId &&            
                           //entegre zaman 
                           (x.IsIntegrated && (x.StartDate.Date <= adviceDay.Date && x.EndDate.Date >= adviceDay.Date))
                           // entegre olmayan zaman kontrolu
                           || (!x.IsIntegrated &&
                               (x.StartDate <= adviceDay && x.EndDate >= adviceDay && x.IncludedDate.DateInCombination(adviceDay) &&
                                   (
                                       (x.StartTime != null &&
                                       x.StartTime.Value.TimeOfDay <= adviceDay.TimeOfDay)
                                       && (x.EndTime != null && x.EndTime.Value.TimeOfDay >= adviceDay.TimeOfDay)
                                   )
                               )
                           )
                    );
            //Öğrenci uygun olmadıgı zaman varsa müsait değil olarak dönüş yapılır.
            //Kayıt olmaması öğrenci icin müsaitlik anlamına gelir.
            // return availableTimes.Any(x => !x.IsAvaible) ? false : true;
            return availableTimes;


        }
        public object GetAdvices2()
        {

            var currentSOperation = _shuttleOperationRepo.Table
                                .Include(x => x.ShuttleStudentOperations)
                                .ThenInclude(x => x.Student)
                                .FirstOrDefault(x => x.Id == 11078);
            DateTime adviceDay = currentSOperation != null ? currentSOperation.DateTime : DateTime.Now;

            var times = GetStudentAvaiblableTimes(adviceDay);

            var resultStudent = currentSOperation.ShuttleStudentOperations
            .Select(x => new
            {
                x.Student.Id,
                x.Student.Name,
                x.Student.Surname,


            });


            var result = (from student in resultStudent
                          join time in times on student.Id equals time.Student.Id into studentTimes
                          from time in studentTimes.DefaultIfEmpty()
                          select new
                          {
                              student,
                              time
                          }
                          ).GroupBy(x => x.student).Select(x => new
                          {
                              x.Key.Id,
                              x.Key.Name,
                              x.Key.Surname,
                              IsAvaible = x.Any(y => y.time == null ? false : !y.time.IsAvaible) ? false : true
                          }).ToList();

            // availableTimes.Any(x => !x.IsAvaible) ? false : true;
            // var res = (from advice in addVice
            //            join adres in adress on advice.StudentId equals adres.StudentId
            //            select new
            //            {
            //                advice.StudentId,
            //                advice.DisContinuityCount,
            //                advice.MounthlyDiscontinuityCount,
            //                adres.Name,
            //                adres.Surname,
            //                adres.LocationX,
            //                adres.LocationY
            //            }).OrderByDescending(x => x.DisContinuityCount)


            return result;

        }

        public void SetAdviceToOperation(long studentId, long shuttleOperationId)
        {

            // var student=studentRepo.
            var student = _studentRepo.FindBy(studentId);
            if (student == null)
            {
                throw new ShuttleOperationException("Öğrenci Bilgisi Bulunamadı!");
            }

            var shuttleOperation = _shuttleOperationRepo.FindBy(shuttleOperationId);
            if (shuttleOperation == null)
            {
                throw new ShuttleOperationException("Servis operasyon bilgisi bulunamadı!");
            }

            if (shuttleOperation.ShuttleOperationStatus == ShuttleOperationStatus.OprationFinished)
            {
                throw new ShuttleOperationException("Bitmiş servis operasyonu için öneri eklenemez!");
            }

            if (shuttleOperation.ShuttleStudentOperations.Any(x => x.Student.Id == studentId))
            {
                throw new ShuttleOperationException("Mevctu Servis operasyonu içerisinde seçili öğrenci bulunmakta!");


            }


            var studentOperation = new ShuttleStudentOperation()
            {
                Student = student,
                ShuttleOperation = shuttleOperation,
                IsCompensation = true,
                LessonRelation = new ShuttleStudentOperasionLessonRelation
                {
                    CompletedLessonCount = 0,
                    PlannedLessonCount = 0
                }

            };
            _studentOperationRepo.Add(studentOperation);

        }

    }

    public class StudentAdress
    {
        public long StudentId { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }

    }
}