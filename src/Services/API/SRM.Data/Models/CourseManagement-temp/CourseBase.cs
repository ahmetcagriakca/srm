//using System;
//using System.Collections.Generic;
//using SRM.Model.Data.Activities;

//namespace SRM.Data.Models.CourseManagement
//{
//	/// <summary>
//	/// 
//	/// </summary>
//	public class CourseBase : FixEntity<Guid>
//    {
//        public string Code { get; set; }
//        public DateTime Begin { get; set; }
//        public DateTime End { get; set; }
//        public bool ScheduleStatus { get; set; }

//    }
//    /// <summary>
//    /// planlanan ders içerği
//    /// </summary>
//    public class ExpectedCourse : CourseBase
//    {
//        /// <summary>
//        /// Plan tamamlandı mı
//        /// </summary>
//        public bool IsCompleted { get; set; }
//        /// <summary>
//        /// Plan İptal Edildi mi
//        /// </summary>
//        public bool IsCanceled { get; set; }
//        /// <summary>
//        /// Plan Yayınlandı mı
//        /// </summary>
//        public bool IsPublished { get; set; }
//        public ICollection<StudentAssignmentsExpected> Assigments { get; set; }
//    }

//    public class ActualCourse : CourseBase
//    {
//        public ICollection<StudentAssignmentsActual> Assigments { get; set; }
//    }

//}
