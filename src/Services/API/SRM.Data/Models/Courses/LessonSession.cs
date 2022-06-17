using Fix.Data;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.InstructorManagement;
using SRM.Data.Models.Individuals.StudentManagement;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Courses
{

    /// <summary>
    /// işlenen dersin seans bilgisi
    /// </summary>
    public class LessonSession : FixEntity<long>
    {
        /// <summary>
        /// Ders
        /// </summary>
        /// <value>The lesson.</value>
        public Lesson Lesson { get; set; }

        /// <summary>
        /// Derse seansı bazlı öğretmen tanımı yapılacak.Bunun sebebi öğrenci-öğretmen 
        /// öneceliklendirmeleri sebebiyle derse girecek olan öğretmenin değişkenlik
        /// göstermesindendir.
        /// </summary>
        /// <value>The instructor.</value>
        public Instructor Instructor { get; set; }

        /// <summary>
        /// Seansın Başlık Bilgisi
        /// </summary>
        /// <value>The header.</value>
        public string Header { get; set; }

        /// <summary>
        /// Dersin içerik Bilgisi
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Seansın Başlama Zamanı 
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Seans içerik dökümanları
        /// </summary>
        /// <value>The lesson content documents.</value>
        public ICollection<StudentLessonSession> StudentLessonSessions { get; set; }
        /// <summary>
        /// Seans içerik dökümanları
        /// </summary>
        /// <value>The lesson content documents.</value>
        public ICollection<Document> LessonContentDocuments { get; set; }

    }
}
