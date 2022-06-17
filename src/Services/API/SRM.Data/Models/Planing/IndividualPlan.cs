using SRM.Data.Models.Courses.Parameters;
using SRM.Data.Models.Individuals;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Planing
{
    public class IndividualPlan : PlanBase<Guid>
    {
        /// <summary>
        /// Öğretmen
        /// </summary>
        public Individual Individual { get; set; }

        /// <summary>
        /// Plan Tarihi Starttime
        /// </summary>
        public DateTime StartTime { get; set; }

        /// <summary>
        /// Derslik
        /// </summary>
        /// <value>The class room.</value>
        public ClassRoom ClassRoom { get; set; }

        /// <summary>
        /// Öğrenci planları
        /// </summary>
        /// <value>The students lesson</value>
        public ICollection<StudentLessonPlan> StudentLessonPlans { get; set; }


    }
}
