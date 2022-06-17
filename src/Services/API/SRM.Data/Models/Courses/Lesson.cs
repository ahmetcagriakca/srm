using Fix.Data;
using SRM.Data.Models.Courses.Parameters;
using System.Collections.Generic;

namespace SRM.Data.Models.Courses
{
    /// <summary>
    /// Ders 
    /// </summary>
    public class Lesson : FixEntity<long>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        /// <summary>
        /// Dersin Branş Bilgisi
        /// </summary>
        /// <value>The branch.</value>
        public Branch Branch { get; set; }

        /// <summary>
        /// Ders Seansları. Öğretmen Bilgisine seans üzerinden erişilir.
        /// </summary>
        /// <value>The lesson sessions.</value>
        public ICollection<LessonSession> LessonSessions { get; set; }
    }
}
