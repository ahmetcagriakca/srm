using Fix;
using Fix.Data;
using Fix.Logging;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain.Individuals.Exceptions.BaseException;
using SRM.Domain.Individuals.Parameters.Services;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Domain.Shuttles.Parameters.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentService : IStudentService
    {
        private readonly IRepository<Student> _studentRepository;
        private readonly IRepository<StudentObstacleType> _studentObstacleTypeRepository;
        private readonly IRepository<LocationRegion> locationRegionRepository;
        private readonly IObstacleTypeService obstacleTypeService;
        private readonly ILocationRegionService locationRegionService;
        private readonly IShuttleTemplateService shuttleTemplateService;
        private readonly ITransactionManager transactionManager;
        private readonly ILogManager logManager;

        public StudentService(IRepository<Student> studentRepository,
            IRepository<StudentObstacleType> studentObstacleTypeRepository,
            IRepository<Address> addressRepository,
            IRepository<LocationRegion> locationRegionRepository,
            IObstacleTypeService obstacleTypeService,
            ILocationRegionService locationRegionService,
            IShuttleTemplateService shuttleTemplateService,
            ITransactionManager transactionManager,
            ILogManager logManager
            )
        {
            _studentRepository = studentRepository;
            _studentObstacleTypeRepository = studentObstacleTypeRepository;
            this.locationRegionRepository = locationRegionRepository;
            //this.addressRepository = addressRepository;
            this.obstacleTypeService = obstacleTypeService;
            this.locationRegionService = locationRegionService;
            this.shuttleTemplateService = shuttleTemplateService;
            this.transactionManager = transactionManager;
            this.logManager = logManager;
        }

        #region Students
        public IEnumerable<Student> GetStudents()
        {
            return _studentRepository.Table;
        }

        public IQueryable<Student> GetStudentsWithRelations()
        {
            return _studentRepository.Table
                .Include(en => en.Addresses).ThenInclude(en => en.Address).ThenInclude(x => x.Location)
                .Include(en => en.Addresses).ThenInclude(en => en.Address).ThenInclude(x => x.LocationRegion)
                .Include(en => en.ObstacleTypes).ThenInclude(en => en.ObstacleType)
                .Include(en => en.Reports)
                .Include(en => en.StudentContacts);
        }

        public IEnumerable<Student> SearchStudents(long? id, string identityNumber, string name, string surname, int? obstacleType, DateTime? reportStartDate, DateTime? reportEndDate, bool? isActive, int? locationRegionId)
        {
            var result = _studentRepository.Table
                .Include(en => en.ObstacleTypes).ThenInclude(en => en.ObstacleType)
                // .Include(en => en.LocationRegion)
                .Include(en => en.Reports)
                .Include(en => en.Addresses).ThenInclude(x => x.Address).ThenInclude(x => x.LocationRegion)
                .Where(en => (id == null || en.Id == id)
                && (identityNumber.IsNullOrEmpty() || en.IdentityNumber.ToUpper().Contains(identityNumber.ToUpper()))
                && (name.IsNullOrEmpty() || en.Name.ToUpper().Contains(name.ToUpper()))
                && (surname.IsNullOrEmpty() || en.Surname.ToUpper().Contains(surname.ToUpper()))
                && (obstacleType == null || en.ObstacleTypes.Any(eno => eno.ObstacleType.Id == obstacleType))
                //&& (reportEndDate == null || en.Reports.Any(eno => eno.EndDate < reportEndDate))
                && (isActive == null || en.IsActive == isActive)
                && (locationRegionId == null || en.Addresses.Any(z => z.Address.LocationRegion.Id == locationRegionId))
             ).ToList();


            var res = result.Where(en =>
                              (reportStartDate == null || en.Reports.LastOrDefault(eno => eno.StartDate.Date == reportStartDate.Value.Date) != null)
                             && (reportEndDate == null || en.Reports.LastOrDefault(eno => eno.EndDate.Date == reportEndDate.Value.Date) != null)
                            );
            return res;
        }

        /// <summary>
        /// Öğrenci idsine göre öğrenci getirme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Student GetStudentById(long id)
        {
            return GetStudentsWithRelations()
                .FirstOrDefault(en => en.Id == id);
        }

        /// <summary>
        /// Öğrenci bölgesine göre öğrenci getirme
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IEnumerable<Student> GetStudentByLocationId(int locationId)
        {
            var location = locationRegionService.GetById(locationId);
            var regionids = new List<int> { locationId };
            regionids.AddRange(location.RegionRelations.Select(en => en.SubRegion.Id));

            return GetStudentsWithRelations().Where(x => x.Addresses.Any(y => regionids.Contains(y.Address.LocationRegion.Id)));
        }

        /// <summary>
        /// Kimlik Numarasına göre öğrenci getirme
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        public Student GetStudentByIdentityNumber(string identityNumber)
        {
            var student = _studentRepository.Table.FirstOrDefault(x => x.IdentityNumber == identityNumber);
            if (student == null)
                throw new RecordNotFoundException(identityNumber + " Kimlik Numaralı öğrenci kaydı bulunamadı.");
            return GetStudentById(student.Id);
        }

        /// <summary>
        /// Öğrenci Varmı kontrolü
        /// </summary>
        /// <param name="identityNumber"></param>
        /// <returns></returns>
        public bool IsStudentExist(string identityNumber)
        {
            return _studentRepository.Any(x => x.IdentityNumber == identityNumber);
        }

        /// <summary>
        /// Öğrenci oluşturma
        /// </summary>
        /// <param name="student"></param>
        public void CreateStudent(Student student, List<int> obstacleTypeIdList)
        {
            if (IsStudentExist(student.IdentityNumber))
            {
                throw new RecordNotFoundException($"{student.IdentityNumber} kimlik numaralı öğrenci tanımı daha önce yapılmış");
            }
            student.Name = student.Name.NameToCamelCase();
            student.Surname = student.Surname.NameToCamelCase();
            student.ParentName = student.ParentName.NameToCamelCase();

            obstacleTypeIdList?.ForEach(en =>
            {
                var obstacleType = obstacleTypeService.GetById(en);

                if (IsStudentExist(student.IdentityNumber))
                {
                    throw new RecordNotFoundException($"{student.IdentityNumber} kimlik numaralı öğrenci tanımı daha önce yapılmış");
                }
                student.ObstacleTypes.Add(new StudentObstacleType() { Student = student, ObstacleType = obstacleType });
            });
            _studentRepository.Add(student);
        }

        /// <summary>
        /// Öğrenci Güncelleme
        /// </summary>
        /// <param name="student"></param>
        public void UpdateStudent(Student student, List<int> obstacleTypeIdList)
        {

            if (student.ObstacleTypes.Count() > 0)
            {
                //var enumerator = student.ObstacleTypes.GetEnumerator();
                obstacleTypeIdList = obstacleTypeIdList ?? new List<int>();
                var list = student.ObstacleTypes.Where(en => !(obstacleTypeIdList.Contains(en.ObstacleType.Id)));
                var length = list.Count();
                for (int i = 0; i < length; i++)
                {
                    var item = list.First();
                    student.ObstacleTypes.Remove(item);
                    obstacleTypeIdList?.Remove(item.ObstacleType.Id);
                }
            }
            obstacleTypeIdList?.ForEach(en =>
            {
                if (!student.ObstacleTypes.Any(eno => eno.ObstacleType.Id == en))
                {
                    var obstacleType = obstacleTypeService.GetById(en);
                    _studentObstacleTypeRepository.Add(new StudentObstacleType() { Student = student, ObstacleType = obstacleType });
                    //student.ObstacleTypes.Add();
                }
            });

            ///bölge değişikliği yapıldıysa taslaklar siliniyor.
            _studentRepository.Update(student);
        }

        public void UpdateStudentLocationRegion(long studentId, int? locationRegionId)
        {
            var student = GetStudentById(studentId);
            if (locationRegionId != null)
                student.LocationRegion = locationRegionService.GetById(locationRegionId.ToInteger());
            else
            {
                student.LocationRegion = null;
            }
            _studentRepository.Update(student);
        }

        /// <summary>
        /// Öğrenci Silme
        /// </summary>
        /// <param name="id"></param>
        public void DeleteStudent(long id)
        {
            var student = GetStudentById(id);
            _studentRepository.Delete(student);
        }

        /// <summary>
        /// Toplu öğrenci yaratma
        /// </summary>
        /// <param name="id"></param>
        public void CreateExcelStudents(List<Student> students)
        {
            foreach (var student in students)
            {
                try
                {
                    //TODO:BU kısım da adres lokasyon bilgisi yeni yapıya göre uygulayalım
                    var existStudent = _studentRepository.Table.FirstOrDefault(en => en.IdentityNumber == student.IdentityNumber);
                    var locationRegion = locationRegionService.GetByCode(student.LocationRegion.Code);
                    if (locationRegion != null)
                    {
                        // student.LocationRegion = locationRegion;
                        student.Addresses.FirstOrDefault().Address.LocationRegion = locationRegion;
                        //TODO:bu kısım patlayabilir
                    }
                    else
                    {
                        locationRegionService.Create(new Data.Models.Shuttles.Parameters.LocationRegion()
                        {
                            Name = student.LocationRegion.Name,
                            Code = student.LocationRegion.Code,
                            IsActive = true
                        });
                        transactionManager.Commit();
                        var newlocationRegion = locationRegionService.GetByCode(student.LocationRegion.Code);
                        //student.LocationRegion = newlocationRegion;
                        student.Addresses.FirstOrDefault().Address.LocationRegion = newlocationRegion;
                    }
                    if (existStudent != null)
                    {
                        existStudent.Name = student.Name;
                        existStudent.Surname = student.Surname;
                        existStudent.DateOfBirth = student.DateOfBirth;
                        existStudent.ParentName = student.ParentName;
                        existStudent.ParentPhoneNumber = student.ParentPhoneNumber;
                        if (existStudent.Addresses?.Count > 0)
                        {
                            var address = existStudent.Addresses.FirstOrDefault();
                            address.Address.AddressInfo = student.Addresses.ToList()[0].Address.AddressInfo;
                        }
                        else
                        {
                            existStudent.Addresses = student.Addresses;
                        }
                        //  existStudent.LocationRegion = student.LocationRegion;
                        UpdateStudent(existStudent, null);
                        transactionManager.Commit();
                    }
                    else
                    {
                        student.IsActive = true;
                        CreateStudent(student, null);
                        transactionManager.Commit();
                    }
                }
                catch (Exception ex)
                {
                    logManager.Logger.Error(ex, "Error on read csv. Identity number:" + student.IdentityNumber);
                }
            }
        }
        #endregion
    }
}


