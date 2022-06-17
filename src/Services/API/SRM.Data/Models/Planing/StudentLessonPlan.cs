using SRM.Data.Models.Individuals.StudentManagement;
using System;

namespace SRM.Data.Models.Planing
{
    /// <summary>
    /// Öğrenci dersi başlayabilimesi için öncesinde ders planı yapılmalı
    /// haftalık task dısı yapıcak dersler icinde ders planı yapılmalı
    /// </summary>
    public class StudentLessonPlan : PlanBase<Guid>
    {
        public Student Student { get; set; }

        /// <summary>
        /// Öğretmen Planı
        /// </summary>
        public IndividualPlan IndividualPlan { get; set; }


    }


}
