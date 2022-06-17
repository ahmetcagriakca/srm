using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Parameters;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Data.Models.Shuttles.TemplateManagement;
using SRM.Domain.Shuttles.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public class ShuttleTemplateService : IShuttleTemplateService
    {
        private readonly IRepository<ShuttleTemplate> shuttleTemplateRepository;
        private readonly IRepository<ShuttleStudentTemplate> shuttleStudentTemplateRepository;
        private readonly IRepository<LocationRegion> locationRegionRepository;
        private readonly IRepository<Student> studentReposityory;
        private readonly IRepository<StudentService> studentServiceRepository;
        private readonly IRepository<ApplicationParameter> appParameterRepo;


        public ShuttleTemplateService(
            IRepository<ShuttleTemplate> _shuttleTemplateRepository,
            IRepository<ShuttleStudentTemplate> _shuttleStudentTemplateRepository,
            IRepository<LocationRegion> _locationRegionRepository,
            IRepository<Student> _studentReposityory,
            IRepository<StudentService> studentServiceRepository,
            IRepository<ApplicationParameter> _appParameterRepo
            )
        {
            shuttleTemplateRepository = _shuttleTemplateRepository;
            shuttleStudentTemplateRepository = _shuttleStudentTemplateRepository;
            locationRegionRepository = _locationRegionRepository;
            studentReposityory = _studentReposityory;
            this.studentServiceRepository = studentServiceRepository;
            appParameterRepo = _appParameterRepo;
        }


        public ShuttleTemplate GetById(int id)
        {
            return shuttleTemplateRepository
                    .Table
                .Include(x => x.LocationRegion)
                .Include(x => x.StudentService)
                .Include(x => x.ShuttleStudentTemplates)
                .ThenInclude(x => x.Student)
                    .FirstOrDefault(en => en.Id == id);
        }

        //Servis taslağı varmı
        public bool IsShuttleTemplateExist(int regionId, int dayOfWeek, TimeSpan time)
        {
            return shuttleTemplateRepository
                .Table
                .Include(x => x.LocationRegion)
                .Any(x => x.LocationRegion.Id == regionId && x.DayOfWeek == dayOfWeek && x.Time.Ticks == time.Ticks && x.IsActive && !x.IsDelete);
        }

        //Servis Taslagını Ekle
        public void CreateShuttleTemplate(ShuttleTemplate shuttleTemplate, int locationRegionId, int studentServiceId)
        {
            var locationRegion = locationRegionRepository.FindBy(locationRegionId);

            if (locationRegion == null)
                throw new ShuttleOperationTemplateException("Bölge Tanımı Bulunamadı!");
            if (!locationRegion.IsActive)
                throw new ShuttleOperationTemplateException("Pasif bölge için taslak tanımı yapılamaz!");

            if (IsShuttleTemplateExist(locationRegionId, shuttleTemplate.DayOfWeek, shuttleTemplate.Time))
            {
                throw new RecordAlreadyExistException("Servis taslak tanımı seçilen bölge ve zaman için daha önce yapılmış");
            }


            var studentService = studentServiceRepository.FindBy(studentServiceId);

            ShuttleServiceOperationTimeCheck(shuttleTemplate, studentService);

            shuttleTemplate.LocationRegion = locationRegion;
            shuttleTemplate.StudentService = studentService;


            shuttleTemplateRepository.Add(shuttleTemplate);
        }

        public void ShuttleServiceOperationTimeCheck(ShuttleTemplate shuttleTemplate, StudentService studentService)
        {
            var param = appParameterRepo.GetAllWithoutRestriction().FirstOrDefault(x => x.Name == "ShuttleTemplateConstraint" && x.IsActive);
            if (param == null)
                throw new ShuttleOperationException("'ShuttleTemplateConstraint' parametre değeri alınamadı!");
            var timeRangeMinutes = Convert.ToInt32(param.Value);

            //Kendisi gelenler icin kontrolu kaldırdık
            if (studentServiceRepository.Table/*.Include(x => x.Driver)*/.Any(x => x.Id == studentService.Id && x.DriverId == 0))
                return;

            var rTime = new TimeSpan(0, timeRangeMinutes, 0);
            if (shuttleTemplateRepository.Table.
            Include(x => x.StudentService).
            Any(x => x.StudentService.Id == studentService.Id && x.DayOfWeek == shuttleTemplate.DayOfWeek &&
            shuttleTemplate.Time.Add(-rTime) <= x.Time && x.Time <= shuttleTemplate.Time.Add(rTime)
            && x.Id != shuttleTemplate.Id
            && x.IsActive
            && !x.IsDelete))
            {
                throw new RecordAlreadyExistException($"{studentService.Plate} plakalı servise {timeRangeMinutes} dk içerisinde 1 den fazla operasyon taslağı tanımlanamaz!");

            }

        }

        //Servis Taslagını Ekle
        public void UpdateShuttleTemplate(ShuttleTemplate entity, int locationRegionId, int studentServiceId)
        {
            var template = GetById(entity.Id);

            var studentService = studentServiceRepository.FindBy(studentServiceId);

            if (template.StudentService.Id != studentService.Id || entity.Time != template.Time
            || entity.IsActive != template.IsActive
            )
            {
                template.Time = entity.Time;
                template.IsActive = entity.IsActive;
                ShuttleServiceOperationTimeCheck(entity, studentService);
                template.StudentService = studentService;
                shuttleTemplateRepository.Update(template);
            }

        }

        //Öğrenci servis taslagı varmı
        public void IsStudentTemplateExist(ShuttleTemplate shuttleTemplate, Student student)
        {

            if (shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.Student).Any(x => x.ShuttleTemplate.Id == shuttleTemplate.Id && x.Student.Id == student.Id))
                throw new RecordAlreadyExistException($"{student.Name + " " + student.Surname } için servis taslak tanımı seçilen servis taslağı için daha önce yapılmış");

            if (shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.Student).Any(x => x.ShuttleTemplate.DayOfWeek == shuttleTemplate.DayOfWeek && x.ShuttleTemplate.IsActive && x.IsActive && x.Student.Id == student.Id))
                throw new RecordAlreadyExistException($"{student.Name + " " + student.Surname }  için taslak tanım aynı gün içerisinde mevcut!");

        }
        //Öğrenci servis taslagını ekle
        public void CreateStudentTemplate(int shuttleTemplateId, long studentId, ShuttleStudentTemplate studentTemplate)
        {
            var student = studentReposityory.FindBy(studentId);
            var shuttleTemp = shuttleTemplateRepository.Table.Include(en => en.StudentService).FirstOrDefault(en => en.Id == shuttleTemplateId);

            IsStudentTemplateExist(shuttleTemp, student);

            if (shuttleTemp.StudentService.MaxCapacity <= shuttleTemp.ShuttleStudentTemplates.Where(x => x.IsActive).Count())
                throw new ShuttleOperationTemplateException("Servis kapasitesinden fazla öğrenci eklenemez!");
            //var checkOrder = shuttleTemplateRepository.Table.Include(en => en.ShuttleStudentTemplates)
            //    .Any(en => en.Id == shuttleTemplateId
            //               && en.ShuttleStudentTemplates.Any(sst => sst.Order == studentTemplate.Order));
            //if (checkOrder)
            //    throw new ShuttleOperationTemplateException("Servise bu sıraya sahip öğrenci daha önce eklenmiş başka sıra ile ekleyiniz.");
            studentTemplate.ShuttleTemplate = shuttleTemp;
            studentTemplate.Student = student;
            shuttleStudentTemplateRepository.Add(studentTemplate);

        }

        public void UpdateStudentTemplate(long studentTemplateId, int order, int lessonCount)
        {
            var studentTemp = shuttleStudentTemplateRepository.FindBy(studentTemplateId);
            studentTemp.Order = order;
            studentTemp.LessonCount = lessonCount;

            //var checkOrder = shuttleTemplateRepository.Table.Include(en => en.ShuttleStudentTemplates)
            //    .Any(en => en.Id == studentTemp.Id
            //               && en.ShuttleStudentTemplates.Any(sst => sst.Id != studentTemplateId && sst.Order == order));
            //if (checkOrder)
            //    throw new ShuttleOperationTemplateException("Bu sıraya sahip öğrenci bulunuyor başka sıra ile güncelleyiniz.");
            //shuttleStudentTemplateRepository.Update(studentTemp);

        }

        //Servis Taslagını sil
        public void DeleteShuttleTemplate(int shuttleTemplateId)
        {
            var template = shuttleTemplateRepository.FindBy(shuttleTemplateId);
            template.IsDelete = true;
            // shuttleTemplateRepository.Update(template);

            // shuttleTemplateRepository.Delete(shuttleTemplateRepository.FindBy(shuttleTemplateId));

        }

        //Öğrenci Taslagını sil
        public void DeleteStudentTemplate(long studentTemplateId)
        {
            shuttleStudentTemplateRepository.Delete(shuttleStudentTemplateRepository.FindBy(studentTemplateId));
        }

        //Bütün servis taslaklarını getir
        public IEnumerable<ShuttleTemplate> GetAllShuttleTemplate()
        {
            return shuttleTemplateRepository.Table.Where(x => !x.IsDelete).Include(x => x.LocationRegion).OrderBy(x => x.Time).OrderBy(x => x.DayOfWeek);

        }

        //Günlük servis taslaklarını getir
        public IEnumerable<ShuttleTemplate> GetShuttleTemplateByDayOfWeek(int? dayOfWeek)
        {
            return shuttleTemplateRepository
                .Table
                .Include(x => x.ShuttleStudentTemplates)
                .ThenInclude(x => x.Student)
                .Include(x => x.LocationRegion)
                .Include(x => x.StudentService)
                .Where(x => (dayOfWeek == null || x.DayOfWeek == dayOfWeek) && !x.IsDelete)
                .OrderBy(x => x.DayOfWeek);
        }

        //Servis taslagının öğrenci taslaklarını getir
        public IEnumerable<ShuttleStudentTemplate> GetStudentTemplateByShuttleTemplateId(int shuttleTemplateId)
        {
            return shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).ThenInclude(x => x.LocationRegion).Include(x => x.Student).Where(x => x.ShuttleTemplate.Id == shuttleTemplateId).OrderBy(x => x.Order);
        }

        //Öğrenci taslagını id ye göre getir
        public ShuttleStudentTemplate GetStudentTemplateById(int studentTemplateId)
        {
            return shuttleStudentTemplateRepository.Table.Include(x => x.Student).FirstOrDefault(x => x.Id == studentTemplateId);
        }
        public void DeleteStudentTemplateByStudentId(long studentId)
        {
            var studentShuttleTemplates = shuttleStudentTemplateRepository.Table.Include(en => en.Student).Where(x => x.Student.Id == studentId).ToList();
            studentShuttleTemplates.ForEach(en => { shuttleStudentTemplateRepository.Delete(en); });
        }

        //Bölgenin ilişkili oldugu alt bölge tanımı kaldırılken taslaklarda alt bölgeden gelen öğrenci varsa sildirmeyiz
        public void CheckDeleteSubRegion(int regionId, int subRegionId)
        {
            var temp = shuttleTemplateRepository.Table.Include(x => x.ShuttleStudentTemplates).ThenInclude(x => x.Student).ThenInclude(x => x.Addresses).Include(x => x.LocationRegion).Where(x => !x.IsDelete && x.LocationRegion.Id == regionId && x.ShuttleStudentTemplates.Any(y => y.Student.Addresses.Count(z => x.LocationRegion.Id == subRegionId) > 0));

            if (temp.Count() > 0)
            {
                var main = locationRegionRepository.FindBy(regionId);
                var sub = locationRegionRepository.FindBy(subRegionId);

                throw new ShuttleOperationTemplateException($"{main.Name} bölgesine ait servis taslak kayıtlarında {sub.Name} bölgesinde bulunan öğrenciler bulunduğu için bu işlemi gerçekleştiremezsiniz!");
            }


        }

    }
}