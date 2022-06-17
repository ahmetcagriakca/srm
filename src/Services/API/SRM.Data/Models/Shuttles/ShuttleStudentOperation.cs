using Fix.Data;
using SRM.Data.Models.CallManagement;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Data.Models.Shuttles
{
    /// <summary>
    /// Öğrenci Servis operasyonları
    /// </summary>
    public class ShuttleStudentOperation : FixEntity<long>
    {
        public ShuttleStudentOperation()
        {
            StudentPhoneCalls = new HashSet<StudentPhoneCall>();
            StudentOperationLocations = new HashSet<StudentOperationLocation>();

        }
        /// <summary>
        /// Servis operasyon işlemi
        /// </summary>
        /// <value>The shuttle operation.</value>
        public ShuttleOperation ShuttleOperation { get; set; }

        /// <summary>
        /// Öğrenci
        /// </summary>
        /// <value>The student.</value>
		public Student Student { get; set; }

        /// <summary>
        /// Geldi gelmedi bilgisi
        /// </summary>
        /// <value><c>true</c> if status; otherwise, <c>false</c>.</value>
		public bool? Status { get; set; }//TODO: operasyon statusları tasındıktan sonra kaldırılmalı


        /// <summary>
        ///  Geldi,Gelmedi,Gelecek,Gelmeyecek durumu
        /// </summary>
        /// <returns></returns>
        public ShuttleStudentOperationStatus OperationStatus { get; set; }

        /// <summary>
        /// As , yedek
		/// Telafi için mi geldi sorusuna cevap
        /// </summary>
        /// <value><c>true</c> if is compensation; otherwise, <c>false</c>.</value>
		public bool IsCompensation { get; set; }

        // Servis  Öğrenci sırası
        public int Order { get; set; }

        // //Ders Sayısı
        // public int LessonCount { get; set; }

        public ShuttleStudentOperasionLessonRelation LessonRelation { get; set; }

        public ICollection<StudentPhoneCall> StudentPhoneCalls { get; set; }

        public ICollection<StudentOperationLocation> StudentOperationLocations { get; set; }


    }

    //TODO: opeasyon statusunu genişletebiliriz.Kısıt girildiğinde iptal edilme durumunda karısıklık olabilir.
    public enum ShuttleStudentOperationStatus
    {
        //Planlandı
        Planned = 0,

        //Geldi 
        Come = 1,

        //Gelmedi
        DontCome = 2,

        //Gelecek
        WillComing = 3,

        //Gelmeyecek
        WontComing = 4
    }

    //Öğrencinin okula geldiği zaman katıldıgı ders ilişkileri
    public class ShuttleStudentOperasionLessonRelation : FixEntity<long>
    {
        public long ShuttleStudentOperationRef { get; set; }
        //Öğrenci servis operasyonu
        public ShuttleStudentOperation ShuttleStudentOperation { get; set; }

        //Planlanan ders sayısı
        public int PlannedLessonCount { get; set; }

        //Girilen ders sayısı
        public int CompletedLessonCount { get; set; }

        //TODO:Bu noktadan sonra derslere geçiş yapıyoruz icerik planlamayla buraya bağlıycagız
        //Ders modelleri incelenip ilişkiyi düzenleyebiliriz

        //Öğrenci ders seans ilişkilendirmesi
        // public ICollection<LessonSession> LessonSession { get; set; }

    }
}
