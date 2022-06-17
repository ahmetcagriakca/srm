using Fix.Data;

namespace SRM.Data.Models.Shuttles
{
    public class StudentOperationLocation : FixEntity<long>
    {
        //Öğrenci servis operasyonu
        public ShuttleStudentOperation StudentOperation { get; set; }

        //Lokasyon Enlem
        public string LocationX { get; set; }

        //Lokasyon Boylam
        public string LocationY { get; set; }
    }
}