using Fix.Data;
using SRM.Data.Models.Courses.Parameters;

namespace SRM.Data.Models.Individuals.InstructorManagement
{

    /// <summary>
    /// Öğretment Branş ilişkilendirmesi
    /// </summary>
    public class InstructorBranch : FixEntity<int>
    {
        /// <summary>
        /// Öğretmen bilgisi
        /// </summary>
        /// <value>The instructor.</value>
        public Instructor Instructor { get; set; }

        /// <summary>
        /// Branş Bilgisi
        /// </summary>
        /// <value>The branch.</value>
        public Branch Branch { get; set; }
    }
}
