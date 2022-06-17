using Fix.Data;
using SRM.Data.Models.Shuttles.TemplateManagement;
using System.Collections.Generic;

namespace SRM.Data.Models.Shuttles.Parameters
{
    /// <summary>
    /// TODO:Student Service ne arkadaş heryerde shuttle dedik bunu bir ara mutlaka düzeltelim isimlendirmelerde kafa karıştırıyor StudentService diye Domain katmanında obje bulunuyor.
    /// </summary>
    public class StudentService : ParameterEntity<int>
    {
        //Plaka
        public string Plate { get; set; }

        //Servisin Max Öğrenci Kapasitesi        
        public int MaxCapacity { get; set; }

        //Sofor kullanıcısı
        public long DriverId { get; set; }

        //Servise bağlı servis operasyon taslakları
        public ICollection<ShuttleTemplate> ShuttleTemplates { get; set; }

        //Servise bağlı servis operasyonları
        public ICollection<ShuttleOperation> ShuttleOperations { get; set; }

    }
}