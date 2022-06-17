using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using System;
using System.Collections.Generic;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public interface IStudentService : IDependency
    {
        #region Students
        bool IsStudentExist(string IdentityNumber);

        Student GetStudentById(long id);
        IEnumerable<Student> GetStudentByLocationId(int locationId);

        IEnumerable<Student> GetStudents();

        IEnumerable<Student> SearchStudents(long? id, string identityNumber, string name, string username, int? obstacleType, DateTime? reportStartDate, DateTime? reportEndDate, bool? isActive, int? locationRegionId);

        void CreateStudent(Student student, List<int> obstacleTypeIdList);

        void UpdateStudent(Student student, List<int> obstacleTypeIdList);

        void DeleteStudent(long id);

        Student GetStudentByIdentityNumber(string IdentityNumber);

        void CreateExcelStudents(List<Student> students);

        /// <summary>
        /// Öğrenci bölgesi güncelleme
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="locationRegionId"></param>
        void UpdateStudentLocationRegion(long studentId, int? locationRegionId);
        #endregion
    }
}
