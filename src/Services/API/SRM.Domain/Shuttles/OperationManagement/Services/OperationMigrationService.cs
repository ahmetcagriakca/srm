using Fix;
using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles;
using SRM.Data.Models.Shuttles.TemplateManagement;
using System;
using System.Linq;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{

    public interface IOperationMigrationService : IDependency
    {
        /// <summary>
        /// Taslak ders sayısı bulma
        /// </summary>
        void TemplateAddLessonCount();
        void LessonRealationAddMigration();

        /// <summary>
        /// Öğrenci servis operasyon yeni durumları güncelleme
        /// </summary>
        void SetNewStudentOperationStatus();
        void SetStudentNewContacts();

        /// <summary>
        /// Operasyon lokasyon bilgisi öğrenci adres bilgisine tasır
        /// </summary>
        void OperationLocationMigration();

    }

    public class OperationMigrationService : IOperationMigrationService
    {

        private readonly IRepository<ShuttleTemplate> shuttleTemplateRepository;
        private readonly IRepository<ShuttleStudentTemplate> studenTemplateRepository;
        private readonly IRepository<ShuttleOperation> shuttleOperationRepository;
        private readonly IRepository<ShuttleStudentOperation> shuttleStudentOperationRepository;

        private readonly IRepository<ShuttleStudentOperasionLessonRelation> studentOperastonLessonRelation;

        private readonly IRepository<StudentOperationLocation> studentOperationLocationRepo;
        private readonly IRepository<StudentAddress> studentAddressRepo;

        private readonly IRepository<Student> studentRepo;

        private readonly ITransactionManager transactionManager;
        public OperationMigrationService(IRepository<ShuttleTemplate> _shuttleTemplateRepository, IRepository<ShuttleStudentTemplate> _studenTemplateRepository,
        IRepository<ShuttleOperation> _shuttleOperationRepository,
        IRepository<ShuttleStudentOperasionLessonRelation> _studentOperastonLessonRelation,
        IRepository<ShuttleStudentOperation> _shuttleStudentOperationRepository, IRepository<Student> _studentRepo,
        IRepository<StudentOperationLocation> _studentOperationLocationRepo, IRepository<StudentAddress> _studentAddressRepo,
         ITransactionManager _transactionManager)
        {
            shuttleTemplateRepository = _shuttleTemplateRepository;
            shuttleOperationRepository = _shuttleOperationRepository;
            studentOperastonLessonRelation = _studentOperastonLessonRelation;
            transactionManager = _transactionManager;
            shuttleStudentOperationRepository = _shuttleStudentOperationRepository;
            studenTemplateRepository = _studenTemplateRepository;
            studentRepo = _studentRepo;
            studentOperationLocationRepo = _studentOperationLocationRepo;
            studentAddressRepo = _studentAddressRepo;
        }


        public void TemplateAddLessonCount()
        {
            foreach (var temp in studenTemplateRepository.Table.Include(x => x.Student).Where(x => x.ShuttleTemplate != null))
            {
                var st = studenTemplateRepository.FindBy(temp.Id);

                var lessonCount = studenTemplateRepository.Table.Include(x => x.Student).Any(x => x.Id != temp.Id && x.Student.Id == temp.Student.Id && x.ShuttleTemplate != null) ? 1 : 2;
                st.LessonCount = lessonCount;
                studenTemplateRepository.Update(st);

            }


        }

        public void LessonRealationAddMigration()
        {
            var studentOpList = shuttleStudentOperationRepository.Table.Include(x => x.LessonRelation).Include(x => x.ShuttleOperation).ThenInclude(x => x.ShuttleTemplate).ThenInclude(x => x.ShuttleStudentTemplates).Include(x => x.Student)
            .Where(x => x.LessonRelation == null);

            var plannedLessonCount = 0;
            var completedLessonCount = 0;

            foreach (var stOp in studentOpList)
            {

                var studentTemplate = studenTemplateRepository.Table.Include(x => x.ShuttleTemplate).Include(x => x.Student).Where(x => x.ShuttleTemplate.Id == stOp.ShuttleOperation.ShuttleTemplate.Id && x.Student.Id == stOp.Student.Id).FirstOrDefault();


                if (studentTemplate == null)
                {
                    plannedLessonCount = 0;
                    completedLessonCount = 1;
                }
                else
                {
                    plannedLessonCount = studentTemplate.LessonCount;
                    completedLessonCount = stOp.Status == true ? studentTemplate.LessonCount : 0;
                }


                var nOp = shuttleStudentOperationRepository.FindBy(stOp.Id);
                nOp.LessonRelation = new ShuttleStudentOperasionLessonRelation
                {
                    PlannedLessonCount = plannedLessonCount,//studentTemplate.LessonCount,
                    CompletedLessonCount = completedLessonCount// stOp.Status == true ? studentTemplate.LessonCount : 0
                };



                shuttleStudentOperationRepository.Update(nOp);
            }
        }


        public void SetNewStudentOperationStatus()
        {
            var studentOpList = shuttleStudentOperationRepository.Table.Include(x => x.LessonRelation).Include(x => x.ShuttleOperation).ThenInclude(x => x.ShuttleTemplate).ThenInclude(x => x.ShuttleStudentTemplates).Include(x => x.Student)
          ;

            foreach (var stOp in studentOpList)
            {
                var nOp = shuttleStudentOperationRepository.FindBy(stOp.Id);

                switch (nOp.Status)
                {
                    case true:
                        nOp.OperationStatus = ShuttleStudentOperationStatus.Come;
                        break;
                    case false:
                        nOp.OperationStatus = ShuttleStudentOperationStatus.DontCome;
                        break;
                    default:
                        nOp.OperationStatus = ShuttleStudentOperationStatus.Planned;
                        break;
                }




                shuttleStudentOperationRepository.Update(nOp);

            }
        }

        public void SetStudentNewContacts()
        {
            foreach (var student in studentRepo.Table.Include(x => x.StudentContacts).Where(x => !string.IsNullOrEmpty(x.ParentPhoneNumber)))
            {
                if (student.StudentContacts?.Count() == 0)
                {
                    var numbers = student.ParentPhoneNumber.Split("  ");
                    int say = 1;
                    foreach (var number in numbers)
                    {
                        if (string.IsNullOrEmpty(number.Trim()))
                            continue;

                        student.StudentContacts.Add(new StudentContact
                        {
                            Name = say == 1 ? student.ParentName : student.ParentName + " - " + say,
                            Number = number.Trim()

                        });
                        say++;

                    }

                    studentRepo.Update(student);
                }
            }

        }


        public void OperationLocationMigration()
        {
            foreach (var student in studentRepo.Table.Include(x => x.Addresses)
                        //.Where(x => x.Id == 193)
                        .Where(x => x.Addresses.Any(y => y.Address.Location == null))
                                    )
            {
                var locations = studentOperationLocationRepo.Table
                        .Include(x => x.StudentOperation)
                        .Where(x => x.StudentOperation.Student.Id == student.Id).ToList();

                if (locations.Count() == 0)
                    continue;

                var listX = locations.GroupBy(x => x.LocationX.Substring(0, x.LocationX.Length < 7 ? x.LocationX.Length : 7).ToUpper(), (alphabet, subList) => new
                {
                    Alphabet = alphabet,
                    SubList = subList.OrderBy(x => x.LocationX).ToList()
                })
                .OrderByDescending(x => x.SubList.Count).ToList();

                decimal total = 0;
                foreach (var item in listX.First().SubList)
                {
                    total += Convert.ToDecimal(item.LocationX);
                }
                var avarageX = total / listX.First().SubList.Count;


                var listy = locations.GroupBy(x => x.LocationY.Substring(0, x.LocationY.Length < 7 ? x.LocationY.Length : 7).ToUpper(), (alphabet, subList) => new
                {
                    Alphabet = alphabet,
                    SubList = subList.OrderBy(x => x.LocationY).ToList()
                }
                 ).OrderByDescending(x => x.SubList.Count).ToList();


                total = 0;
                foreach (var item in listy.First().SubList)
                {
                    total += Convert.ToDecimal(item.LocationY);
                }
                var avarageY = total / listy.First().SubList.Count;

                var s = studentRepo.FindBy(student.Id);

                var addres = studentAddressRepo.Table.Include(x => x.Address).Where(x => x.Student.Id == student.Id).First();

                addres.Address.Location = new Location
                {
                    Latitude = avarageX.ToString().Substring(0, avarageX.ToString().Length < 10 ? avarageX.ToString().Length : 10),
                    Longitude = avarageY.ToString().Substring(0, avarageY.ToString().Length < 10 ? avarageY.ToString().Length : 10),
                    LocationX = Convert.ToDouble(avarageX.ToString().Substring(0, avarageX.ToString().Length < 10 ? avarageX.ToString().Length : 10)),
                    LocationY = Convert.ToDouble(avarageY.ToString().Substring(0, avarageY.ToString().Length < 10 ? avarageY.ToString().Length : 10)),


                };

                studentAddressRepo.Update(addres);
            }

        }

    }



}