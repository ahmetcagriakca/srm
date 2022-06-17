using Fix.Data;
using SRM.Data.Models.Individuals.StudentManagement;

namespace SRM.Data.Models.Shuttles
{
    public class ShuttleStudentOperationAdvice : FixEntity<long>
    {
        public ShuttleStudentOperationAdvice()
        {

        }
        ///Servis operasyonu
        public ShuttleOperation ShuttleOperation { get; set; }

        //Öğrenci
        public Student Student { get; set; }

        public ShuttleStudentOperation ShuttleStudentOperation { get; set; }

        //Devamsızlık sayısı
        public int DisContinuityCount { get; set; }

        //Aylık devamsızlık sayısı
        public int MounthlyDiscontinuityCount { get; set; }

        //Öneri icin gelebileceği ders sayısı //TODO: önersi ders sayısı ekeleme kapasamı çalışılıp eklenebilir
        // public int LessonCount { get; set; }

        //Öğrenci katılım bilgisi Öneri durumları
        public AdviceStatus AdviceStatus { get; set; }
    }

    //Öneri Durumları
    public enum AdviceStatus
    {
        //Onay bekliyor
        WaitingConfirm = 0,
        //Öneri Kabul edildi
        Accept = 1,
        //Öneri Red Edildi
        Reject = 2,
        //o güne ait başka öneri kabul edildi
        AnotherAdviceConfirm = 3,
        //Servis Max kapasiteye ulaştı
        ServiceFull = 4,

        //Servis operasyonu bittiği icin kapatılan öneriler.Öneriler değerlenmedi anlamı içerir
        ServiceOperationFinished = 5

    }
}
