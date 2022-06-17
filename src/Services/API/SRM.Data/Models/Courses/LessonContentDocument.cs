using Fix.Data;
using SRM.Data.Models.Application;

namespace SRM.Data.Models.Courses
{
    public class LessonContentDocument : FixEntity<long>
    {
        /// <summary>
        /// Ders Seansı
        /// </summary>
        /// <value>The lesson session.</value>
        public LessonSession LessonSession { get; set; }
        /// <summary>
        /// Dokuman
        /// </summary>
        /// <value>The document.</value>
        public Document Document { get; set; }
    }
}
