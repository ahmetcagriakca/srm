using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.InstructorManagement;
using SRM.Domain.Individuals.Exceptions.BaseException;
using SRM.Domain.Individuals.Parameters.Services;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.InstructorManagement.Services
{
    public class InstructorService : IInstructorService
    {
        private readonly IRepository<Instructor> _instructorRepository;
        private readonly IRepository<InstructorBranch> _instructorBranchRepository;
        private readonly IBranchService branchService;

        public InstructorService(IRepository<Instructor> instructorRepository,
            IRepository<InstructorBranch> instructorBranchRepository,
            IBranchService branchService
            )
        {
            _instructorRepository = instructorRepository;
            _instructorBranchRepository = instructorBranchRepository;
            this.branchService = branchService;
        }


        public IQueryable<Instructor> GetInstructorWithRelations()
        {
            return _instructorRepository.Table
                .Include(en => en.Branches)
                .ThenInclude(en => en.Branch)
                .Include(en => en.Addresses)
                .ThenInclude(en => en.Address);
        }

        public Instructor GetInstructorById(long id)
        {
            return GetInstructorWithRelations()
                .FirstOrDefault(x => x.Id == id);
        }

        public Instructor GetInstructorByIdentityNumber(string identityNumber)
        {
            var instructor = GetInstructorWithRelations()
                .FirstOrDefault(x => x.IdentityNumber == identityNumber);
            if (instructor == null)
            {
                throw new RecordNotFoundException(identityNumber + " Kimlik Numaralı öğretment kaydı bulunamadı.");
            }
            return instructor;
        }

        public IEnumerable<Instructor> GetInstructors()
        {
            return _instructorRepository.Table.Where(x => x.IsActive);
        }
        public void CreateInstructor(Instructor instructor)
        {
            if (IsInstructorExist(instructor.IdentityNumber))
            {
                throw new RecordNotFoundException("Öğretmen tanımı daha önce yapılmış");
            }
            //TODO: Create Individual User With Service
            //instructor.User = accountService.CreateIndividualUser(instructor);
            _instructorRepository.Add(instructor);
        }


        public void UpdateInstructor(Instructor instructor)
        {
            //TODO: Update Individual User With Service
            //accountService.UpdateIndividualUser(instructor, instructor.User.Username);
            _instructorRepository.Update(instructor);
        }

        public IEnumerable<Instructor> Search(long? id, string identityNumber, string name, string surname, int? branch, bool? isActive)
        {
            var result = GetInstructorWithRelations().Include(en => en.Branches).ThenInclude(en => en.Branch).Where(en => (id == null || en.Id == id)
                 && (identityNumber.IsNullOrEmpty() || en.IdentityNumber == identityNumber)
                && (name.IsNullOrEmpty() || en.Name == name)
                && (surname.IsNullOrEmpty() || en.Surname == surname)
                && (branch == null || en.Branches.Any(eno => eno.Branch.Id == branch))
                && (isActive == null || en.IsActive == isActive)
             );

            //var result=_studentRepository.Fetch(en => (id != null || en.Id == id)
            // && (identityNumber.IsNullOrEmpty() || en.IdentityNumber == identityNumber)
            // //&& (name.IsNullOrEmpty() || en.Name == identityNumber)
            // //&& (surname.IsNullOrEmpty() || en.Surname == surname)
            ////&& (obstacleType.IsNullOrEmpty() || en.ObstacleType == obstacleType)
            ////&& (reportStartDate != null || en.IdentityNumber == reportStartDate)
            ////&& (reportEndDate != null || en.IdentityNumber == reportEndDate) 
            ////&& (isActive != null || en.IsActive == isActive)
            //, (Orderable<Student> en) => en.Asc<long>(x => x.Id));
            return result;
        }

        public void DeleteInstructor(long id)
        {
            var instructor = GetInstructorById(id);
            _instructorRepository.Delete(instructor);
        }

        public bool IsInstructorExist(string IdentityNumber)
        {
            return _instructorRepository.Any(x => x.IdentityNumber == IdentityNumber);
        }

        /// <summary>
        /// Öğrencinin Engel tiplerinin girişini 
        /// </summary>
        /// <param name="instructor"></param>
        /// <param name="idList"></param>
        public void CreateInstructorBranches(Instructor instructor, List<int> idList)
        {
            idList?.ForEach(en =>
            {
                var branch = branchService.GetById(en);
                _instructorBranchRepository.Add(new InstructorBranch() { Instructor = instructor, Branch = branch });
            });
        }

        public void UpdateInstructorBranches(Instructor instructor, List<int> idList)
        {
            var instructorBranches = _instructorBranchRepository.Table.Where(en => en.Instructor == instructor);

            if (instructorBranches.Count() > 0)
            {
                //var enumerator = student.ObstacleTypes.GetEnumerator();
                var list = instructorBranches.Where(en => !idList.Contains(en.Branch.Id));
                var length = list.Count();
                for (int i = 0; i < length; i++)
                {
                    var item = list.First();
                    _instructorBranchRepository.Delete(item);
                    //studentObstacleTypes.Remove(item);
                    idList.Remove(item.Branch.Id);
                }
            }

            idList?.ForEach(en =>
            {
                if (!instructorBranches.Any(eno => eno.Branch.Id == en))
                {
                    var branch = branchService.GetById(en);
                    _instructorBranchRepository.Add(new InstructorBranch() { Instructor = instructor, Branch = branch });
                    //student.ObstacleTypes.Add();
                }
            });
            //return _studentRepository.Table;
        }
    }
}
