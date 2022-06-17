using Fix;
using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.InstructorManagement;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Parameters;
using SRM.Data.Models.Shuttles;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Data.Models.Shuttles.TemplateManagement;
using SRM.Domain.Individuals.Exceptions.BaseException;
using SRM.Domain.Shuttles.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using SRM.Data.Models.CallManagement;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public class ShuttleService : IShuttleService
    {
        private readonly IRepository<LocationRegion> locationRegionRepository;
        private readonly IRepository<ShuttleTemplate> shuttleTemplateRepository;
        private readonly IRepository<ShuttleOperation> shuttleOperationRepository;
        private readonly IRepository<ShuttleStudentOperation> shuttleStudentOperationRepository;
        private readonly IRepository<LocationRegionRelation> locationRegionRelationRepository;
        private readonly IRepository<Student> studentRepository;
        private readonly IRepository<ShuttleStudentOperationAdvice> shuttleStudentOperationAdviceRepository;
        private readonly IRepository<StudentAvailableTime> studentAvailableTimeReporsitory;
        private readonly IRepository<StudentOperationLocation> studentOperationLocationRepository;
        private readonly IRepository<StudentService> studentServiceReposityory;
        private readonly IRepository<ApplicationParameter> appParameterRepo;
        private readonly ITransactionManager transactionManager;

        private readonly IWorkContext workContext;
        private readonly IRepository<Instructor> instructorRepo;

        private readonly IRepository<ShuttleStudentOperasionLessonRelation> studentOperastonLessonRelation;
        public ShuttleService(
            IRepository<LocationRegion> _locationRegionRepository,
            IRepository<ShuttleTemplate> _shuttleTemplateRepository,
            IRepository<ShuttleOperation> _shuttleOperationRepository,
            IRepository<ShuttleStudentOperation> _shuttleStudentOperationRepository,
            IRepository<LocationRegionRelation> _locationRegionRelationRepository,
            IRepository<Student> _studentRepository,
            IRepository<ShuttleStudentOperationAdvice> _shuttleStudentOperationAdviceRepository,
            IRepository<StudentAvailableTime> _studentAvailableTimeReporsitory, IRepository<StudentOperationLocation> _studentOperationLocationRepository,
            IRepository<StudentService> _studentServiceReposityory,
            ITransactionManager _transactionManager,
            IRepository<ApplicationParameter> _appParameterRepo,
            IRepository<ShuttleStudentOperasionLessonRelation> _studentOperastonLessonRelation,
            IWorkContext _workContext,
            IRepository<Instructor> _instructorRepo
        )
        {
            locationRegionRepository = _locationRegionRepository;
            shuttleTemplateRepository = _shuttleTemplateRepository;
            shuttleOperationRepository = _shuttleOperationRepository;
            shuttleStudentOperationRepository = _shuttleStudentOperationRepository;
            locationRegionRelationRepository = _locationRegionRelationRepository;
            studentRepository = _studentRepository;
            shuttleStudentOperationAdviceRepository = _shuttleStudentOperationAdviceRepository;
            studentAvailableTimeReporsitory = _studentAvailableTimeReporsitory;
            studentOperationLocationRepository = _studentOperationLocationRepository;
            studentServiceReposityory = _studentServiceReposityory;
            transactionManager = _transactionManager;
            appParameterRepo = _appParameterRepo;
            studentOperastonLessonRelation = _studentOperastonLessonRelation;
            workContext = _workContext;
            instructorRepo = _instructorRepo;
        }

        public IQueryable<ShuttleTemplate> GetShuttleTemplateWithRelations()
        {
            return shuttleTemplateRepository.GetAllWithoutRestriction()
                    .Include(x => x.ShuttleStudentTemplates)
                    .ThenInclude(x => x.Student)
                    .Include(e => e.LocationRegion)
                    .Include(x => x.StudentService);
        }

        public IQueryable<ShuttleOperation> GetShuttleOperationWithRelation()
        {
            return shuttleOperationRepository.GetAllWithoutRestriction()
                  .Include(x => x.ShuttleTemplate)
                  .Include(x => x.LocationRegion)
                  .Include(x => x.StudentService);
        }


        //Haftalık ders planları olusturma
        public bool CreateWeeklyShuttleOperation(DateTime weekStartTime)
        {
            var startDay = weekStartTime.DayOfWeek;

            //TODO:Tatil kontrolü eklenmeli

            for (int day = startDay.ToInteger(); day <= 7; day++)
            {
                //Lokasyon aktif mi kontrolü ekledik
                //öğrenci servisi aktif mi kontrolü ekledik
                var shuttleTemplates = GetShuttleTemplateWithRelations()
                    .Where(x => x.DayOfWeek == day && x.IsActive && !x.IsDelete
                            && x.LocationRegion.IsActive && x.StudentService.IsActive);
                foreach (var tmp in shuttleTemplates)
                {
                    // var operataionTime = weekStartTime.AddDays(tmp.DayOfWeek - 1).AddHours(tmp.HourOfDate);

                    var operationTime = weekStartTime.AddDays(tmp.DayOfWeek - 1).AddMinutes(tmp.Time.TotalMinutes);

                    var shuttleOperation = new ShuttleOperation()
                    {
                        ShuttleTemplate = tmp,
                        DateTime = operationTime,
                        LocationRegion = tmp.LocationRegion,
                        StudentService = tmp.StudentService,
                        CompanyId = tmp.CompanyId

                    };

                    if (IsShuttleOperationExist(shuttleOperation))
                        continue;

                    //bölgeye ait öğrenciler üzerinde zamanlama olarak uygun olanlar icin operasyon olusturulur.
                    foreach (var studentTemplate in tmp.ShuttleStudentTemplates.Where(x => x.Student.IsActive))
                    {
                        var studentOperation = new ShuttleStudentOperation()
                        {
                            Student = studentTemplate.Student,
                            IsCompensation = false,
                            Order = studentTemplate.Order,
                            LessonRelation = new ShuttleStudentOperasionLessonRelation
                            {
                                PlannedLessonCount = studentTemplate.LessonCount,
                                CompletedLessonCount = 0,
                                CompanyId = studentTemplate.CompanyId
                            },
                            CompanyId = studentTemplate.CompanyId
                        };
                        //öğrenciler icin zaman uygunluk kontrolu yapılacak
                        if (IsStudentAvaiblable(studentTemplate.Student.Id, operationTime))
                        {
                            shuttleOperation.ShuttleStudentOperations.Add(studentOperation);
                        }

                    }
                    shuttleOperationRepository.Add(shuttleOperation);

                    // transactionManager.Rollback();
                    transactionManager.Commit();

                }

            }
            return true;
        }
        public bool IsShuttleOperationExist(ShuttleOperation operation)
        {
            var isExist = GetShuttleOperationWithRelation()
                .Any(x => x.ShuttleTemplate.Id == operation.ShuttleTemplate.Id && x.DateTime.Date == operation.DateTime.Date);
            return isExist;
        }

        public bool CreateShuttleOperationByTemplateId(long shuttleOperationTemplateId)
        {
            var operationDate = DateTime.Now.Date;
            // weekStartTime.DayOfWeek;

            //TODO:Tatil kontrolü eklenmeli

            //Lokasyon aktif mi kontrolü ekledik
            //öğrenci servisi aktif mi kontrolü ekledik
            var shuttleTemplate = shuttleTemplateRepository.Table
                .Include(x => x.ShuttleStudentTemplates).ThenInclude(x => x.Student)
                .Include(e => e.LocationRegion).Include(x => x.StudentService).FirstOrDefault(x => x.Id == shuttleOperationTemplateId
                && x.IsActive && !x.IsDelete && x.LocationRegion.IsActive && x.StudentService.IsActive);
            var tmp = shuttleTemplate;

            var operationTime = operationDate.Date.AddMinutes(tmp.Time.TotalMinutes);

            var shuttleOperation = new ShuttleOperation()
            {
                ShuttleTemplate = tmp,
                DateTime = operationTime,
                LocationRegion = tmp.LocationRegion,
                StudentService = tmp.StudentService
            };

            //bölgeye ait öğrenciler üzerinde zamanlama olarak uygun olanlar icin operasyon olusturulur.
            foreach (var studentTemplate in tmp.ShuttleStudentTemplates)
            {
                var studentOperation = new ShuttleStudentOperation()
                {
                    Student = studentTemplate.Student,
                    IsCompensation = false,
                    Order = studentTemplate.Order,
                    LessonRelation = new ShuttleStudentOperasionLessonRelation
                    {
                        PlannedLessonCount = studentTemplate.LessonCount,
                        CompletedLessonCount = 0
                    }
                };
                //öğrenciler icin zaman uygunluk kontrolu yapılacak
                if (IsStudentAvaiblable(studentTemplate.Student.Id, operationTime))
                {
                    shuttleOperation.ShuttleStudentOperations.Add(studentOperation);
                }

            }
            shuttleOperationRepository.Add(shuttleOperation);

            return true;
        }

        //Öğrenci servis operasyonu yada önerisi için uygunmu kontrolu yapılır
        public bool IsStudentAvaiblable(long studentId, DateTime dateTime)
        {
            //TODO: Öğrenci zaman uyugunlukları kontrol edilecek
            var availableTimes = studentAvailableTimeReporsitory.Table.Include(x => x.Student).Include(x => x.IncludedDate).
            Where(x => x.Student.Id == studentId &&
               (
                   //entegre zaman 
                   (x.IsIntegrated && (x.StartDate <= dateTime && x.EndDate >= dateTime))
                   // entegre olmayan zaman kontrolu
                   ||
                   (!x.IsIntegrated &&
                       (x.StartDate <= dateTime && x.EndDate >= dateTime && x.IncludedDate.DateInCombination(dateTime) &&
                           (
                               (x.StartTime != null && x.StartTime.Value.TimeOfDay <= dateTime.TimeOfDay) && (x.EndTime != null && x.EndTime.Value.TimeOfDay >= dateTime.TimeOfDay)
                           )
                       )
                   )
               )
            );
            //Öğrenci uygun olmadıgı zaman varsa müsait değil olarak dönüş yapılır.
            //Kayıt olmaması öğrenci icin müsaitlik anlamına gelir.
            return availableTimes.Any(x => !x.IsAvaible) ? false : true;
        }

        /// <summary>
        /// Öğrenci operasyon katılım bilgisini set eder
        /// </summary>
        /// <param name="shuttleStudentOperationId"></param>
        /// <param name="ComingStatus"></param>
        /// <returns></returns>
        public void SetStudentShuttleOperationStatus(long shuttleStudentOperationId, ShuttleStudentOperationStatus ComingStatus)
        {
            var studentOperation = shuttleStudentOperationRepository.Table.Include(en => en.ShuttleOperation).Include(x => x.LessonRelation).FirstOrDefault(en => en.Id == shuttleStudentOperationId);

            switch (studentOperation.ShuttleOperation.ShuttleOperationStatus)
            {
                case ShuttleOperationStatus.Waiting: //Operasyon başlamadı
                    if (!(ComingStatus == ShuttleStudentOperationStatus.WillComing || ComingStatus == ShuttleStudentOperationStatus.WontComing))
                        throw new ShuttleOperationFinishedException("Servis operasyonu başlamadan öğrenci katılım durumu güncellenemez.");
                    break;
                case ShuttleOperationStatus.InOperation: //Operasyon esnasında
                    //Bu durumda kontrol edilebilicek durumlar gözden geçirip eklenebilir
                    break;
                case ShuttleOperationStatus.OprationFinished:
                    var userId = workContext.AuthenticationProvider.GetUserId();

                    ///TODO:User Role Id Check will be modified
                    //if (!securityDomain.Account.GetUsersByRoles("System Administrator").Any(x => x.Id == userId))
                    //{
                    //    throw new ShuttleOperationFinishedException("Servis tamamlanmış işlem yapılamaz.");
                    //}
                    break;

                default:
                    break;
            }

            studentOperation.OperationStatus = ComingStatus;

            if (ComingStatus == ShuttleStudentOperationStatus.DontCome)
            {
                studentOperation.LessonRelation.CompletedLessonCount = 0;
            }

            shuttleStudentOperationRepository.Update(studentOperation);

        }

        public void CreateAdvice(DateTime date, long shuttleOpID)
        {
            //Eski tarihe olusturmayalım
            if (date.Date < DateTime.Now.Date)
                throw new AdviceCreateException("Geçmiş tarihlere öneri oluşturulamaz!");

            var shuttles = shuttleOperationRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.LocationRegion).ThenInclude(x => x.RegionRelations).ThenInclude(x => x.SubRegion).Include(x => x.ShuttleStudentOperations).ThenInclude(x => x.Student)
                .Include(x => x.StudentService)/*.ThenInclude(x => x.Driver)*/
                .Where(x => (x.Id == shuttleOpID || x.DateTime.Date == date.Date) &&
                   x.ShuttleOperationStatus == ShuttleOperationStatus.Waiting);

            foreach (var shuttle in shuttles)
            {
                //Kendisi gelenler icin öneri oluşturmuyoruz
                if (shuttle.StudentService.DriverId == 0)
                    continue;

                //Servis kapasitesi doluysa öneri üretmeyelim
                if (!ShuttleCapacityStatus(shuttle.Id))
                    continue;

                var relationRegions = shuttle.LocationRegion.RegionRelations;

                //Bölgenin kendi icerisindeki önerilen öğrencileri öneri olarak ekler
                CreateRegionAdvice(shuttle, shuttle.LocationRegion);

                foreach (var region in relationRegions.Where(x => x.SubRegion.IsActive))
                {
                    CreateRegionAdvice(shuttle, region.SubRegion);
                }
            }
        }

        //Bölgeler icin öğrenci servis önerilerini oluşturur
        //isCurrentRegionShuttle kendi bölgesinde öneri aradığını belirtir
        private void CreateRegionAdvice(ShuttleOperation shuttle, LocationRegion region)
        {

            var param = appParameterRepo.GetAllWithoutRestriction().FirstOrDefault(x => x.Name == "ShuttleAdviceCheckDate" && x.IsActive);
            if (param == null)
                throw new ShuttleOperationException("'ShuttleAdviceCheckDate' parametre değeri alınamadı!");

            int shuttleAdviceCheckDate = Convert.ToInt32(param.Value) * -1;

            var opAdviceList = shuttleStudentOperationRepository.Table.Include(x => x.LessonRelation)
                .Include(x => x.ShuttleOperation)
                .Where(x => x.ShuttleOperation.DateTime.Date.Date >= shuttle.DateTime.AddDays(shuttleAdviceCheckDate).Date &&
                   x.Student.Addresses.Any(y => y.Address.LocationRegion.Id == region.Id)
                )
                .GroupBy(x => x.Student)
                .Select(z => new
                {
                    StudentId = z.Key.Id,
                    ShuttleOperation = shuttle,
                    DisContinuityCount =
                       z.Where(x => x.OperationStatus != ShuttleStudentOperationStatus.Planned && x.LessonRelation != null).Sum(u => u.LessonRelation.PlannedLessonCount - u.LessonRelation.CompletedLessonCount),
                    MounthlyDiscontinuityCount = z.Where(x => x.OperationStatus != ShuttleStudentOperationStatus.Planned && x.LessonRelation != null && x.ShuttleOperation.DateTime.Year == shuttle.DateTime.Year && x.ShuttleOperation.DateTime.Month == shuttle.DateTime.Month).Sum(u => u.LessonRelation.PlannedLessonCount - u.LessonRelation.CompletedLessonCount)

                    // z.Where(u => u.IsCompensation == false && u.Status == false).Sum(x => x.LessonCount) -
                    // z.Where(u => u.IsCompensation == true && u.Status != null).Sum(x => x.LessonCount),

                });
            //x.Status!=null kontrolünün amacı operasyonu gerceklesenlerden sorgulmak.
            //Öğrenci devamsızlıgında planlanan ve telafi ayrımına LessonRelation tablosundaki farka batımız icin ayırmaya gerek kalmadı.
            //MounthlydiscontinuityCount aylık devamsızlık sayısı

            foreach (var advice in opAdviceList.Where(x => x.DisContinuityCount > 0 &&
                   !shuttle.ShuttleStudentOperations.Any(y => y.Student.Id == x.StudentId) //öğrenci bu operasyonda varmı
                ))
            {

                var newAdvice = new ShuttleStudentOperationAdvice()
                {
                    Student = studentRepository.Table.FirstOrDefault(e => e.Id == advice.StudentId),
                    DisContinuityCount = advice.DisContinuityCount,
                    MounthlyDiscontinuityCount = advice.MounthlyDiscontinuityCount,
                    ShuttleOperation = advice.ShuttleOperation,
                    AdviceStatus = AdviceStatus.WaitingConfirm,
                    //  LessonCount = AdviceLessonCountCalculate() //Öneri ders sayısı ekeleme kapsımını sonraya bırakıyorum.
                };

                if (CheckCreateAdvice(newAdvice))
                    shuttleStudentOperationAdviceRepository.Add(newAdvice);
            }
        }

        private bool CheckCreateAdvice(ShuttleStudentOperationAdvice advice)
        {
            bool result = true;

            //Bu öneri mevcutta üretildiyse bir daha üretilmeyecek
            result = !shuttleStudentOperationAdviceRepository
                .Table.Include(x => x.Student)
                .Include(x => x.ShuttleOperation)
                .Any(x => x.Student.Id == advice.Student.Id && x.ShuttleOperation.Id == advice.ShuttleOperation.Id);
            if (!result)
                return result;

            //Öğrenci bu servis önerisi icin uygunmu?
            result = IsStudentAvaiblable(advice.Student.Id, advice.ShuttleOperation.DateTime);
            if (!result)
                return result;
            //TODO: öneri listesine eklenmeli mi kontrolünü burada yapalım
            //Öğrencinin diğer önerileri kontrol edilebilir
            //Lokasyon bilgisiyle doğrulanabilir
            //Business artabilir düşünelim
            return result;
        }

        public IQueryable<ShuttleStudentOperationAdvice> GetShuttleStudentAdvicesWithRelation(AdviceStatus? status = AdviceStatus.WaitingConfirm)
        {
            var shuttleAdvice = shuttleStudentOperationAdviceRepository
                .Table
                .Include(x => x.ShuttleOperation)
                .ThenInclude(x => x.LocationRegion)
                .Include(x => x.Student)
                .Where(x => (status == null || x.AdviceStatus == status));
            return shuttleAdvice;
        }

        //Günlük öğrenci servis öneri listesi       
        public IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByDate(DateTime date)
        {
            return GetShuttleStudentAdvicesWithRelation()
                .Where(x => x.ShuttleOperation.DateTime.Date == date.Date);
        }
        //Servis operasyonu icin öğrenci önerileri 
        public IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByShuttleOperationId(long serviceOperationId)
        {
            return GetShuttleStudentAdvicesWithRelation()
                .Where(x => x.ShuttleOperation.Id == serviceOperationId);
        }

        //Öğrenciye ait o günlük servis önerileri
        public IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByDateAndStudent(DateTime date, long studentId)
        {
            return GetShuttleStudentAdvicesWithRelation()
                .Where(x => x.Student.Id == studentId &&
                   x.ShuttleOperation.DateTime.Date == date.Date);
        }

        //Öğrenciye ait o günlük servis önerileri
        public ShuttleStudentOperationAdvice GetStudentShuttleAdviceById(int adviceId)
        {
            return GetShuttleStudentAdvicesWithRelation(null)
                .FirstOrDefault(x => x.Id == adviceId);
        }

        //Günlük öğrenci operasyon listesi
        public ShuttleOperation GetStudentShuttleOperationById(int id)
        {
            var operation = shuttleOperationRepository
                .Table
                .Include(x => x.ShuttleStudentOperations)
                .ThenInclude(x => x.Student)
                .Include(x => x.ShuttleStudentOperations).ThenInclude(x => x.LessonRelation)
                .Include(x => x.LocationRegion)
                .Include(x => x.StudentService)
                .FirstOrDefault(x => x.Id == id);
            return operation;
        }

        //Günlük öğrenci operasyon listesi
        public IEnumerable<ShuttleOperation> GetStudentOperationListByDate(DateTime date)
        {
            var operation = shuttleOperationRepository
                .Table
                .Include(x => x.ShuttleStudentOperations)
                .ThenInclude(x => x.Student)
                .Include(x => x.ShuttleStudentOperations).ThenInclude(x => x.LessonRelation)
                .Include(x => x.LocationRegion)
                .Include(x => x.StudentService)
                .Where(x => x.DateTime.Date == date.Date)
                .OrderBy(en => en.DateTime);
            return operation;
        }

        public IEnumerable<ShuttleStudentOperation> GetStudentOperationListByShuttleOperationId(long serviceOperationId)
        {
            var students = shuttleStudentOperationRepository
                .Table.Include(x => x.ShuttleOperation)
                .Include(x => x.LessonRelation)
                .Include(x => x.Student)
                .Where(x => x.ShuttleOperation.Id == serviceOperationId);
            return students;
        }


        //Günlük servis operasyon listesi
        public ShuttleOperation GetShuttleOperationById(int operationId)
        {
            var operation = shuttleOperationRepository
                .Table
                .Include(x => x.LocationRegion)
                .FirstOrDefault(x => x.Id == operationId);
            return operation;
        }
        //Günlük servis operasyon listesi
        public IEnumerable<ShuttleOperation> GetShuttleOperationListByDate(DateTime date)
        {
            var operation = shuttleOperationRepository
                .Table
                .Include(x => x.LocationRegion)
                .Where(x => x.DateTime.Date == date.Date)
                .OrderBy(x => x.DateTime);
            return operation;
        }

        public IEnumerable<ShuttleOperation> GetShuttleOperationListByDateForDriver(DateTime date)
        {
            ///TODO:User Role Id Check will be modified
            //if (!securityDomain.Account.GetUsersByRoles("Driver").Any(x => x.Id == workContext.AuthenticationProvider.GetUserId()))
            //{
            //    throw new ShuttleOperationException("Servis listesini erişebilmeniz için servis şoförü olarak giriş yapılmalı!");
            //}

            ///TODO:User information will be added to student service
            var studentService = studentServiceReposityory.Table/*.Include(x => x.Driver)*/.FirstOrDefault(/*x => x.Driver.Id == workContext.AuthenticationProvider.GetUserId()*/);

            if (studentService == null)
            {
                throw new ShuttleOperationException("Şoföre tanımlı servis bulunamadı!");
            }

            var operation = shuttleOperationRepository
                .Table
                .Include(x => x.LocationRegion).Include(x => x.StudentService)
                .Where(x => x.DateTime.Date == date && x.StudentService.Id == studentService.Id)
                .OrderBy(x => x.DateTime);
            return operation;
        }

        //Servise daha fazla öneri kabul edilebilirmi
        public bool ShuttleCapacityStatus(long shuttleOperationId)
        {
            var shuttle = shuttleOperationRepository
                .Table
                .Include(x => x.ShuttleStudentOperations)
                .Include(x => x.StudentService)
                .FirstOrDefault(x => x.Id == shuttleOperationId);

            return shuttle.StudentService.MaxCapacity > shuttle.ShuttleStudentOperations.Where(x => (x.OperationStatus != ShuttleStudentOperationStatus.DontCome && x.OperationStatus != ShuttleStudentOperationStatus.WontComing)).Count();
        }

        public void SetStudentOperationLocation(long shuttleStudentOperationId, string locationX, string locationY)
        {
            //Lokasyon bilgisi boş geliyorsa kaydetmeyelim
            if (locationX == null || locationY == null)
                return;

            var operationLocation = new StudentOperationLocation()
            {
                StudentOperation = shuttleStudentOperationRepository.FindBy(shuttleStudentOperationId),
                LocationX = locationX,
                LocationY = locationY
            };
            studentOperationLocationRepository.Add(operationLocation);

        }

        public void SetShuttleOperationStatus(long shuttleOparationId, ShuttleOperationStatus status)
        {
            var shuttleOperation = shuttleOperationRepository.FindBy(shuttleOparationId);

            var param = appParameterRepo.GetAllWithoutRestriction().FirstOrDefault(x => x.Name == "ShuttleOperationStartTime" && x.IsActive);
            if (param == null)
                throw new ShuttleOperationException("'ShuttleOperationStartTime' parametre değeri alınamadı!");

            int operationCoverage = Convert.ToInt32(param.Value);

            if (shuttleOperation.DateTime.AddHours(operationCoverage) < DateTime.Now)
                throw new ShuttleOperationException(string.Format("Operasyon zamanını {0} saatten fazla zaman geçtiği için başlatılamaz!", operationCoverage));
            if (shuttleOperation.DateTime.AddHours(-operationCoverage) > DateTime.Now)
                throw new ShuttleOperationException(string.Format("Operasyon zamanına {0} saatten fazla zaman olduğu için operasyon başlatılamaz!", operationCoverage));

            switch (status)
            {
                case ShuttleOperationStatus.InOperation:
                    shuttleOperation.OperationStartTime = DateTime.Now;
                    break;
                case ShuttleOperationStatus.OprationFinished:
                    //Gelen öğrencilerin ders sayıları set edilir
                    foreach (var sOp in shuttleStudentOperationRepository.Table.Include(x => x.ShuttleOperation).Include(x => x.LessonRelation).Where(x => x.ShuttleOperation.Id == shuttleOparationId &&
                         x.OperationStatus == ShuttleStudentOperationStatus.Come))
                    {
                        int complatedLessonCount = 0;
                        if (sOp.IsCompensation)
                        {
                            complatedLessonCount = 1;
                        }
                        else
                        {
                            complatedLessonCount = sOp.LessonRelation.PlannedLessonCount;
                        }

                        SetStudentOperastionLessonsCount(sOp.Id, complatedLessonCount);
                    }

                    var dontComeList = shuttleStudentOperationRepository.Table.Include(x => x.ShuttleOperation).Where(x => x.ShuttleOperation.Id == shuttleOparationId &&
                      (x.OperationStatus != ShuttleStudentOperationStatus.Come && x.OperationStatus != ShuttleStudentOperationStatus.DontCome));

                    //Gelemeyecek durumundaki öğrencileri gelmedi olarak set edelim
                    foreach (var dontComeStudent in dontComeList.Where(x => x.OperationStatus == ShuttleStudentOperationStatus.WontComing))
                    {
                        dontComeStudent.OperationStatus = ShuttleStudentOperationStatus.DontCome;
                        shuttleStudentOperationRepository.Update(dontComeStudent);

                    }

                    if (
                        dontComeList.Any(x => x.OperationStatus != ShuttleStudentOperationStatus.Come
                                              && x.OperationStatus != ShuttleStudentOperationStatus.DontCome
                                              && x.OperationStatus != ShuttleStudentOperationStatus.WontComing))
                    {
                        throw new RecordNotFoundException("Tüm öğrenciler için katılım bilgisi girilmeden Servis Operasyonu tamamlanamaz!");
                    }

                    shuttleOperation.OperationEndTime = DateTime.Now;

                    SetAdviceToOperationFinished(shuttleOperation.Id);

                    break;

            }

            shuttleOperation.ShuttleOperationStatus = (ShuttleOperationStatus)status;
            shuttleOperationRepository.Update(shuttleOperation);
        }

        //Servise ait önerileri servis bitmesi durumunda durumunu değiştirir
        public void SetAdviceToOperationFinished(long shuttleOperationId)
        {
            var operation = shuttleOperationRepository.FindBy(shuttleOperationId);
            if (operation == null)
                throw new ShuttleOperationException("Servis Operasyonu Bulunamadı! {SetAdviceToOperationFinished}");

            if (operation.ShuttleOperationStatus != ShuttleOperationStatus.InOperation)
                throw new ShuttleOperationException("Servis operasyonu başlamadan operasyon önerileri iptal edilemez ! {SetAdviceToOperationFinished}");

            var advices = shuttleStudentOperationAdviceRepository.Table.Include(x => x.ShuttleOperation).Where(x => x.ShuttleOperation.Id == shuttleOperationId && x.AdviceStatus == AdviceStatus.WaitingConfirm);
            foreach (var advice in advices)
            {
                advice.AdviceStatus = AdviceStatus.ServiceOperationFinished;
                shuttleStudentOperationAdviceRepository.Update(advice);
            }

        }

        //Öğrenci uygun olmama durumunda üzerindeki operasyon ve önerileri iptal eder.
        public void ChangeStudentOperationByAvaibleTime(long studentAvailableTimeId)
        {
            var availableTime = studentAvailableTimeReporsitory.Table.Include(x => x.Student).Include(x => x.IncludedDate).FirstOrDefault(x => x.Id == studentAvailableTimeId);
            if (availableTime == null)
                return;

            var studentId = availableTime.Student.Id;
            if (!availableTime.IsAvaible)
            {
                var operations = shuttleStudentOperationRepository.Table.Include(x => x.Student).Include(x => x.ShuttleOperation)
                    .Where(x => x.Student.Id == studentId && x.OperationStatus == ShuttleStudentOperationStatus.Planned &&
                       x.ShuttleOperation.AvaibleTimeConditionforShuttleOperation(availableTime)

                    );

                foreach (var operation in operations)
                {
                    var op = shuttleStudentOperationRepository.FindBy(operation.Id);
                    // op.Status = false;
                    op.OperationStatus = ShuttleStudentOperationStatus.DontCome;

                    shuttleStudentOperationRepository.Update(op);
                }

                var advices = shuttleStudentOperationAdviceRepository.Table.Include(x => x.Student).Include(x => x.ShuttleOperation)
                    .Where(x =>
                       x.Student.Id == studentId &&
                       x.AdviceStatus != AdviceStatus.Reject &&
                       x.ShuttleOperation.AvaibleTimeConditionforShuttleOperation(availableTime));
                foreach (var advice in advices)
                {
                    var ad = shuttleStudentOperationAdviceRepository.FindBy(advice.Id);
                    ad.AdviceStatus = AdviceStatus.Reject;

                    shuttleStudentOperationAdviceRepository.Update(ad);
                }

            }

        }

        public long CreateCustomShuttleOperation(DateTime operationDateTime, int regionId, int studentServiceId)
        {
            var region = locationRegionRepository.Table.FirstOrDefault(x => x.Id == regionId);
            if (region == null)
                throw new ShuttleOperationException("Bölge bilgisi bulunamadı!");
            if (!region.IsActive)
                throw new ShuttleOperationException("Pasif bölgeler için operasyon eklenemez!");

            var studentServise = studentServiceReposityory.Table.FirstOrDefault(x => x.Id == studentServiceId);
            if (studentServise == null)
                throw new ShuttleOperationException("Servis araç bilgisi bulunamadı!");
            if (!studentServise.IsActive)
                throw new ShuttleOperationException("Pasif servis araçları için operasyon eklenemez!");

            //Tarih geçmişmi kontrolü istenilerek eklenmedi.Sebebi geçmiş tarihler için daha sonradan sisteme giriş yapılması icin 

            //Öğrenci servisi icin belirtilen tarihte operasyon varmı kontrolü
            if (studentServiceReposityory.Table.Include(x => x.ShuttleOperations).Any(x => x.Id == studentServiceId && x.ShuttleOperations.Count(y => y.DateTime == operationDateTime) > 0))
                throw new ShuttleOperationException("Servis aracı için belirtilen zamanda servis operasyonu mevcut!");

            var operation = new ShuttleOperation()
            {
                DateTime = operationDateTime,
                LocationRegion = region,
                StudentService = studentServise

            };
            shuttleOperationRepository.Add(operation);
            transactionManager.Commit();
            return operation.Id;
        }

        public void CreateCustomStudentOperation(long studentId, long shuttleOperationId, int lessonCount)
        {
            var student = studentRepository.FindBy(studentId);
            if (student == null)
                throw new ShuttleOperationException("Öğrenci bilgisi bulunamadı!");
            if (!student.IsActive)
                throw new ShuttleOperationException("Pasif öğrenciler için operasyon oluşturulamaz!");

            //Operasyon icin geçmişe yönelik öğrenci eklenebilir
            var operation = shuttleOperationRepository.FindBy(shuttleOperationId);

            if (operation == null)
                throw new ShuttleOperationException("Operasyon bilgileri boş geçilemez!");

            if (shuttleStudentOperationRepository.Table.Include(x => x.Student).Include(x => x.ShuttleOperation).Any(x => x.Student.Id == studentId && x.ShuttleOperation.DateTime.Date == operation.DateTime.Date))
                throw new ShuttleOperationException("Öğrencinin aynı gün içeresinde operasyonu mevcut.Yeni Operasyon eklenemez!");

            //Burada bölge kontrolü yapılmadı.Sebebi ise farklı lokasyondan öğrenci eklenebilir gibi bişey düşündüm ;)

            //TODO: öğrenci zaman uygunlugu kontrol etmeli miyiz? müsait zaman gelince düşünelim.

            var studentOperastion = new ShuttleStudentOperation()
            {
                Student = student,
                ShuttleOperation = operation,
                IsCompensation = true,
                OperationStatus = ShuttleStudentOperationStatus.Come,
                LessonRelation = new ShuttleStudentOperasionLessonRelation
                {
                    PlannedLessonCount = 0,
                    CompletedLessonCount = lessonCount
                }
            };

            shuttleStudentOperationRepository.Add(studentOperastion);

        }

        //İlişkiden kaldırılan bölgeler icin önerileri silme
        public void DeleteSubRegionAdvice(int regionId, int subRegionId)
        {
            var deletedAdvices = shuttleStudentOperationAdviceRepository.Table.Include(x => x.Student).Include(x => x.ShuttleOperation).ThenInclude(x => x.LocationRegion).Where(x => x.AdviceStatus == AdviceStatus.WaitingConfirm && x.ShuttleOperation.LocationRegion.Id == regionId &&
            x.Student.Addresses.Any(y => y.Address.LocationRegion.Id == subRegionId)
            );
            foreach (var item in deletedAdvices)
            {
                shuttleStudentOperationAdviceRepository.Delete(shuttleStudentOperationAdviceRepository.FindBy(item.Id));

            }
        }

        //Öğrenci operasyon gerçeklesen ders sayısı girme
        public void SetStudentOperastionLessonsCount(long shuttleStudentOperationId, int completedLessonCount)
        {
            var lessonRel = studentOperastonLessonRelation.Table.Include(x => x.ShuttleStudentOperation).FirstOrDefault(x => x.ShuttleStudentOperation.Id == shuttleStudentOperationId);
            lessonRel.CompletedLessonCount = completedLessonCount;

            studentOperastonLessonRelation.Update(lessonRel);

        }
        public void SetStudentShuttleOperationStatusForDriver(long shuttleStudentOperationId, ShuttleStudentOperationStatus comingStatus, string locationX, string locationY)
        {
            SetStudentShuttleOperationStatus(shuttleStudentOperationId, comingStatus);

            var studentOperation = shuttleStudentOperationRepository.FindBy(shuttleStudentOperationId);

            if (comingStatus == ShuttleStudentOperationStatus.Come)
            {
                // int complatedLessonCount = 0;
                // if (studentOperation.IsCompensation)
                // {
                //     complatedLessonCount = 1;
                // }
                // else
                // {
                //     complatedLessonCount = GetStudentPlannedLesson(shuttleStudentOperationId);
                // }

                // SetStudentOperastionLessonsCount(shuttleStudentOperationId, complatedLessonCount);

                SetStudentOperationLocation(shuttleStudentOperationId, locationX, locationY);
            }
        }

        public IEnumerable<ShuttleStudentOperation> GetInstructorStudents(DateTime date)
        {
            if (date == null)
            {
                date = DateTime.Now.Date;
            }

            ///TODO:User Role Id Check will be modified
            //if (!securityDomain.Account.GetUsersByRoles("Instructor").Any(x => x.Id == workContext.AuthenticationProvider.GetUserId()))
            //{
            //    throw new ShuttleOperationException("Eğitmen Yetkiniz Yok!");
            //}

            ///TODO:User information will be added to instructor
            var instructor = instructorRepo.Table/*.Include(x => x.User).FirstOrDefault(x => x.User.Id == workContext.AuthenticationProvider.GetUserId())*/.FirstOrDefault();

            var result = shuttleStudentOperationRepository.Table
                .Include(x => x.ShuttleOperation)
                .Include(x => x.Student).ThenInclude(x => x.InstructorRelations)
                .Include(x => x.StudentPhoneCalls)
                .Where(x => x.ShuttleOperation.DateTime.Date == date.Date && x.Student.InstructorRelations.Any(y => y.Instructor == instructor));
            var c = result.ToList().Count;
            return result;

        }

        public IEnumerable<ShuttleStudentOperation> GetStudentOperations(long studentId)
        {
            var result = shuttleStudentOperationRepository.Table
            .Include(x => x.Student)
            .Include(x => x.ShuttleOperation)
            .Include(x => x.LessonRelation)
            .Where(x => x.Student.Id == studentId && x.ShuttleOperation.DateTime.Date >= DateTime.Now.AddDays(-90).Date);

            return result;

        }


        public IEnumerable<ShuttleStudentOperation> GetShuttleOperationStudentLocations(long shuttleOperationId)
        {

            var shuttleStudents = shuttleStudentOperationRepository.Table
                .Include(en => en.Student)
                .ThenInclude(en => en.Addresses)
                .ThenInclude(en => en.Address.Location)
                .Include(en => en.ShuttleOperation)
                .ThenInclude(en => en.LocationRegion)
                .Include(en => en.StudentOperationLocations)
                .Where(en => en.ShuttleOperation.Id == shuttleOperationId);

            return shuttleStudents;
        }

        //Öğrenci servis önersini operasyona dönüştürme
        public ShuttleStudentOperation SetAdviceToOperation(long operationId, long studentId, string description)
        {
            if (!ShuttleCapacityStatus(operationId))
                throw new ShuttleOperationException("Servis kapasitesi dolduğu icin işlem yapılamamakta.!");
            var student = studentRepository.FindBy(studentId);
            var shuttleOperation = shuttleOperationRepository.FindBy(operationId);

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
            shuttleStudentOperationRepository.Add(studentOperation);
            return studentOperation;
        }

        public ShuttleStudentOperation GetShuttleStudentOperationByStudent(long operationId, long studentId)
        {
            var shuttleStudentOperation = shuttleStudentOperationRepository.Table.FirstOrDefault(en =>
                en.ShuttleOperation.Id == operationId && en.Student.Id == studentId);

            if (shuttleStudentOperation == null)
                throw new ShuttleOperationException("Servise ait öğrenci bilgisi bulunamadı");
            return shuttleStudentOperation;
        }
    }
}