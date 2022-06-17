using Fix.Data;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles;

namespace SRM.Data.Models.CallManagement
{
    /// <summary>
    /// Öğrenci Telefon arama bilgileri
    /// </summary>
    /// <typeparam name="long"></typeparam>
    public class StudentPhoneCall : FixEntity<long>
    {
        /// <summary>
        /// Öğrenci
        /// </summary>
        /// <returns></returns>
        public Student Student { get; set; }

        /// <summary>
        /// Arama Açıklması
        /// </summary>
        /// <returns></returns>
        public string Description { get; set; }

        /// <summary>
        /// Arama Tipi
        /// </summary>
        /// <returns></returns>
        public CallType CallType { get; set; }

        /// <summary>
        /// Öğrenci Operasyonu
        /// </summary>
        /// <returns></returns>
        public ShuttleStudentOperation ShuttleStudentOperation { get; set; }

        public StudentAnswer StudentAnswer { get; set; }
    }

    public enum CallType
    {
        Default = 0,
        //Operasyon için yapılan arama
        Operation = 1,
        //Öneri için yapılan arama
        Advice = 2
    }

    public enum StudentAnswer
    {
        //Öğrencinin geleceği durum
        StudentWillCome = 0,
        //Öğrencinin gelmeyeceği durum
        StudentWillNotCome = 1,
    }
}