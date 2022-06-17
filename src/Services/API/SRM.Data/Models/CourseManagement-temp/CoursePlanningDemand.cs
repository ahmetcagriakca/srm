//using System;
//using System.Collections.Generic;
//using SRM.Model.Data.Activities;
//using SRM.Model.Data.IndividualManagement;

//namespace SRM.Data.Models.CourseManagement
//{
//	public class CoursePlanningDemand
//    {
//        public string Description { get; set; }
//        public string Reason { get; set; }
//        public int Priority { get; set; }
//        public IList<CourseSession> Sessions { get; set; }
//        public IList<CoursePlanningStudent> Students { get; set; }
//        public IList<CoursePlanningAssumption> Assumptions { get; set; }
//        public IList<CoursePlanningRecommendedAssignment> RecommendedAssignments { get; set; }
//    }

//	public class CourseSession : FixEntity<Guid>
//    {
//        public IList<Session> Sessions { get; set; }
//    }

//	public class CoursePlanningStudent : FixEntity<Guid>
//    {
//        public IList<Student> Students { get; set; }

//    }

//	public class CoursePlanningAssumption : FixEntity<Guid>
//    {
//        public CoursePlanningDemand Plan { get; set; }
//        public int MaxSessionPerDay { get; set; }
//        public int MaxSessionPerWeek { get; set; }
//    }

//    public class CoursePlanningRecommendedAssignment : Assignment
//    {
//        public CoursePlanningDemand Plan { get; set; }
//        public bool CanIgnore { get; set; }
//    }

//}
