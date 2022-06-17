using Fix.Data;
using SRM.Data.Models.Courses.Parameters;
using SRM.Data.Models.Individuals.InstructorManagement;
using System;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentInstructorRelation : FixEntity<long>
    {
        /// <summary>
        /// Öğrenci
        /// </summary>
        /// <value>The student.</value>
        public Student Student { get; set; }

        /// <summary>
        /// Öğretmen
        /// </summary>
        /// <value>The instructor.</value>
        public Instructor Instructor { get; set; }

        /// <summary>
        /// İlişki Önceliği
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }

        /// <summary>
        /// Ders Branşı
        /// </summary>
        /// <value>The branch.</value>
        public Branch Branch { get; set; }

        /// <summary>
		/// İlişki Başlama Tarihi
        /// </summary>
        /// <value>The start date.</value>
		public DateTime StartDate { get; set; }

    }
}
