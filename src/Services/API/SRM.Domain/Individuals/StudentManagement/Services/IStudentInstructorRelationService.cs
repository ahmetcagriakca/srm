using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentInstructorRelationService : IDependency
    {
        /// <summary>
        /// Öğrencinin öğretmen ilişkilerini getirir
        /// </summary>
        /// <param name="studentId"></param>
        /// <returns></returns>
        IEnumerable<StudentInstructorRelation> GetStudentInstructorRelations(long studentId);

        /// <summary>
        /// Öğretmenin öğrenci ilişkikerini getirir
        /// </summary>
        /// <param name="instructorId"></param>
        /// <returns></returns>
        IEnumerable<StudentInstructorRelation> GetStudentInstructorRelationsForInstructor(long instructorId);

        StudentInstructorRelation GetStudentInstructorRelationById(long id);

        void CreateStudentInstructorRelation(StudentInstructorRelation StudentInstructorRelation);

        void UpdateStudentInstructorRelation(StudentInstructorRelation StudentInstructorRelation);

        void DeleteStudentInstructorRelation(long id);

    }
}
