using SRM.Data.Models.Courses;
using SRM.Data.Models.Individuals.StudentManagement;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Individuals.InstructorManagement
{
    public class Instructor : Individual
    {
        public Instructor()
        {
            Addresses = new HashSet<InstructorAddress>();
            LessonSessions = new HashSet<LessonSession>();
            StudentRelations = new HashSet<StudentInstructorRelation>();
            Branches = new HashSet<InstructorBranch>();

        }

        /// <summary>
        /// Öğretmen Başlama Tarihi
        /// </summary>
        public DateTime HireDate { get; set; }

        /// <summary>
        /// Öğretmenin Adresleri
        /// </summary>
        public ICollection<InstructorAddress> Addresses { get; set; }

        /// <summary>
        /// Öğretmenin girdiği dersler
        /// </summary>
        public ICollection<LessonSession> LessonSessions { get; set; }

        /// <summary>
        /// Öğretmen öğrenci ilişkilendirmesi
        /// </summary>
        /// <value>The instructor relation.</value>
        public ICollection<StudentInstructorRelation> StudentRelations { get; set; }

        /// <summary>
        /// Öğetmenin sahip olduğu branşlar
        /// </summary>
        /// <value>The branches.</value>
		public ICollection<InstructorBranch> Branches { get; set; }
    }
}
