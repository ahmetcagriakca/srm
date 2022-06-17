using Fix.Data;
using SRM.Data.Models.Individuals.StudentManagement;

namespace SRM.Data.Models.Planing.Template
{

    /// <summary>
    /// Öğrenci Ders Plan taslağı
    /// </summary>
    public class StudentLessonPlanTemplate : FixEntity<long>
    {
        /// <summary>
        /// Öğrenci
        /// </summary>
        /// <value>The student.</value>
        public Student Student { get; set; }

        /// <summary>
        /// Öğretmen Plan Taslağı
        /// </summary>
        /// <value>The individual task.</value>
        public IndividualPlanTemplate IndividualPlanTemplate { get; set; }
    }
}