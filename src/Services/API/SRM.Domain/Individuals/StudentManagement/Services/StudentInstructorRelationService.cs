using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentInstructorRelationService : IStudentInstructorRelationService
    {
        private readonly IRepository<StudentInstructorRelation> StudentInstructorRelationRepository;

        public StudentInstructorRelationService(
            IRepository<StudentInstructorRelation> StudentInstructorRelationRepository)
        {
            this.StudentInstructorRelationRepository = StudentInstructorRelationRepository ?? throw new ArgumentNullException(nameof(StudentInstructorRelationRepository));
        }
        private IQueryable<StudentInstructorRelation> GetStudentInstructorRelationWithRelations()
        {
            var results = StudentInstructorRelationRepository.Table
                           .Include(en => en.Student)
                           .Include(en => en.Branch)
                           .Include(en => en.Instructor);
            return results;

        }

        #region Student 
        public IEnumerable<StudentInstructorRelation> GetStudentInstructorRelations(long studentId)
        {
            var results = GetStudentInstructorRelationWithRelations().Where(x => x.Student.Id == studentId);
            return results;
        }
        #endregion

        #region Instructor 
        public IEnumerable<StudentInstructorRelation> GetStudentInstructorRelationsForInstructor(long instructorId)
        {
            var results = GetStudentInstructorRelationWithRelations().Where(x => x.Instructor.Id == instructorId);
            return results;
        }

        #endregion


        public StudentInstructorRelation GetStudentInstructorRelationById(long id)
        {
            var result = GetStudentInstructorRelationWithRelations().FirstOrDefault(en => en.Id == id);
            return result;
        }

        public void CreateStudentInstructorRelation(StudentInstructorRelation StudentInstructorRelation)
        {
            StudentInstructorRelationRepository.Add(StudentInstructorRelation);
        }

        public void UpdateStudentInstructorRelation(StudentInstructorRelation StudentInstructorRelation)
        {
            StudentInstructorRelationRepository.Update(StudentInstructorRelation);
        }

        public void DeleteStudentInstructorRelation(long id)
        {
            var relation = StudentInstructorRelationRepository.FindBy(id);
            StudentInstructorRelationRepository.Delete(relation);
        }

    }
}
