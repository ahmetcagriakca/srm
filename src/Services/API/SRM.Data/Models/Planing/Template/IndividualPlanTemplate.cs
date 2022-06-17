using Fix.Data;
using SRM.Data.Models.Courses.Parameters;
using SRM.Data.Models.Individuals;
using System.Collections.Generic;

namespace SRM.Data.Models.Planing.Template
{
    public class IndividualPlanTemplate : FixEntity<long>
    {
        /// <summary>
        /// Öğretmen
        /// </summary>
        /// <value>The individual.</value>
        public Individual Individual { get; set; }

        /// <summary>
        /// Şablon Hafatanın hangi günü
        /// </summary>
        /// <value>The date.</value>
        public int DayOfWeek { get; set; }

        /// <summary>
        /// Şablon Günün Hangi Saati
        /// </summary>
        /// <value>The hour.</value>
        public int HourOfDay { get; set; }

        /// <summary>
        /// Derslik
        /// </summary>
        /// <value>The class room.</value>
        public ClassRoom ClassRoom { get; set; }

        /// <summary>
        /// Öğrenci plan taslakları
        /// </summary>
        /// <value>The students lesson tasks.</value>
        public ICollection<StudentLessonPlanTemplate> StudentsLessonPlanTemplates { get; set; }
    }

}
