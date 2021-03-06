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

        //Servis tasla???? varm??
        public bool IsShuttleTemplateExist(int regionId, int dayOfWeek, TimeSpan time)
        {
            return shuttleTemplateRepository
                .Table
                .Include(x => x.LocationRegion)
                .Any(x => x.LocationRegion.Id == regionId && x.DayOfWeek == dayOfWeek && x.Time.Ticks == time.Ticks && x.IsActive && !x.IsDelete);
        }

        //Servis Taslag??n?? Ekle
        public void CreateShuttleTemplate(ShuttleTemplate shuttleTemplate, int locationRegionId, int studentServiceId)
        {
            var locationRegion = locationRegionRepository.FindBy(locationRegionId);

            if (locationRegion == null)
                throw new ShuttleOperationTemplateException("B??lge Tan??m?? Bulunamad??!");
            if (!locationRegion.IsActive)
                throw new ShuttleOperationTemplateException("Pasif b??lge i??in taslak tan??m?? yap??lamaz!");

            if (IsShuttleTemplateExist(locationRegionId, shuttleTemplate.DayOfWeek, shuttleTemplate.Time))
            {
                throw new RecordAlreadyExistException("Servis taslak tan??m?? se??ilen b??lge ve zaman i??in daha ??nce yap??lm????");
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
                throw new ShuttleOperationException("'ShuttleTemplateConstraint' parametre de??eri al??namad??!");
            var timeRangeMinutes = Convert.ToInt32(param.Value);

            //Kendisi gelenler icin kontrolu kald??rd??k
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
                throw new RecordAlreadyExistException($"{studentService.Plate} plakal?? servise {timeRangeMinutes} dk i??erisinde 1 den fazla operasyon tasla???? tan??mlanamaz!");

            }

        }

        //Servis Taslag??n?? Ekle
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

        //????renci servis taslag?? varm??
        public void IsStudentTemplateExist(ShuttleTemplate shuttleTemplate, Student student)
        {

            if (shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.Student).Any(x => x.ShuttleTemplate.Id == shuttleTemplate.Id && x.Student.Id == student.Id))
                throw new RecordAlreadyExistException($"{student.Name + " " + student.Surname } i??in servis taslak tan??m?? se??ilen servis tasla???? i??in daha ??nce yap??lm????");

            if (shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.Student).Any(x => x.ShuttleTemplate.DayOfWeek == shuttleTemplate.DayOfWeek && x.ShuttleTemplate.IsActive && x.IsActive && x.Student.Id == student.Id))
                throw new RecordAlreadyExistException($"{student.Name + " " + student.Surname }  i??in taslak tan??m ayn?? g??n i??erisinde mevcut!");

        }
        //????renci servis taslag??n?? ekle
        public void CreateStudentTemplate(int shuttleTemplateId, long studentId, ShuttleStudentTemplate studentTemplate)
        {
            var student = studentReposityory.FindBy(studentId);
            var shuttleTemp = shuttleTemplateRepository.Table.Include(en => en.StudentService).FirstOrDefault(en => en.Id == shuttleTemplateId);

            IsStudentTemplateExist(shuttleTemp, student);

            if (shuttleTemp.StudentService.MaxCapacity <= shuttleTemp.ShuttleStudentTemplates.Where(x => x.IsActive).Count())
                throw new ShuttleOperationTemplateException("Servis kapasitesinden fazla ????renci eklenemez!");
            //var checkOrder = shuttleTemplateRepository.Table.Include(en => en.ShuttleStudentTemplates)
            //    .Any(en => en.Id == shuttleTemplateId
            //               && en.ShuttleStudentTemplates.Any(sst => sst.Order == studentTemplate.Order));
            //if (checkOrder)
            //    throw new ShuttleOperationTemplateException("Servise bu s??raya sahip ????renci daha ??nce eklenmi?? ba??ka s??ra ile ekleyiniz.");
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
            //    throw new ShuttleOperationTemplateException("Bu s??raya sahip ????renci bulunuyor ba??ka s??ra ile g??ncelleyiniz.");
            //shuttleStudentTemplateRepository.Update(studentTemp);

        }

        //Servis Taslag??n?? sil
        public void DeleteShuttleTemplate(int shuttleTemplateId)
        {
            var template = shuttleTemplateRepository.FindBy(shuttleTemplateId);
            template.IsDelete = true;
            // shuttleTemplateRepository.Update(template);

            // shuttleTemplateRepository.Delete(shuttleTemplateRepository.FindBy(shuttleTemplateId));

        }

        //????renci Taslag??n?? sil
        public void DeleteStudentTemplate(long studentTemplateId)
        {
            shuttleStudentTemplateRepository.Delete(shuttleStudentTemplateRepository.FindBy(studentTemplateId));
        }

        //B??t??n servis taslaklar??n?? getir
        public IEnumerable<ShuttleTemplate> GetAllShuttleTemplate()
        {
            return shuttleTemplateRepository.Table.Where(x => !x.IsDelete).Include(x => x.LocationRegion).OrderBy(x => x.Time).OrderBy(x => x.DayOfWeek);

        }

        //G??nl??k servis taslaklar??n?? getir
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

        //Servis taslag??n??n ????renci taslaklar??n?? getir
        public IEnumerable<ShuttleStudentTemplate> GetStudentTemplateByShuttleTemplateId(int shuttleTemplateId)
        {
            return shuttleStudentTemplateRepository.Table.Include(x => x.ShuttleTemplate).ThenInclude(x => x.LocationRegion).Include(x => x.Student).Where(x => x.ShuttleTemplate.Id == shuttleTemplateId).OrderBy(x => x.Order);
        }

        //????renci taslag??n?? id ye g??re getir
        public ShuttleStudentTemplate GetStudentTemplateById(int studentTemplateId)
        {
            return shuttleStudentTemplateRepository.Table.Include(x => x.Student).FirstOrDefault(x => x.Id == studentTemplateId);
        }
        public void DeleteStudentTemplateByStudentId(long studentId)
        {
            var studentShuttleTemplates = shuttleStudentTemplateRepository.Table.Include(en => en.Student).Where(x => x.Student.Id == studentId).ToList();
            studentShuttleTemplates.ForEach(en => { shuttleStudentTemplateRepository.Delete(en); });
        }

        //B??lgenin ili??kili oldugu alt b??lge tan??m?? kald??r??lken taslaklarda alt b??lgeden gelen ????renci varsa sildirmeyiz
        public void CheckDeleteSubRegion(int regionId, int subRegionId)
        {
            var temp = shuttleTemplateRepository.Table.Include(x => x.ShuttleStudentTemplates).ThenInclude(x => x.Student).ThenInclude(x => x.Addresses).Include(x => x.LocationRegion).Where(x => !x.IsDelete && x.LocationRegion.Id == regionId && x.ShuttleStudentTemplates.Any(y => y.Student.Addresses.Count(z => x.LocationRegion.Id == subRegionId) > 0));

            if (temp.Count() > 0)
            {
                var main = locationRegionRepository.FindBy(regionId);
                var sub = locationRegionRepository.FindBy(subRegionId);

                throw new ShuttleOperationTemplateException($"{main.Name} b??lgesine ait servis taslak kay??tlar??nda {sub.Name} b??lgesinde bulunan ????renciler bulundu??u i??in bu i??lemi ger??ekle??tiremezsiniz!");
            }


        }

    }
}