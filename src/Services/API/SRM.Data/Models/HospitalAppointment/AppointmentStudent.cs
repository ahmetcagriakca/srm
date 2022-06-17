using Fix.Data;
using SRM.Data.Models.Individuals.Parameters;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.HospitalAppointment
{
    /// <summary>
    /// Öğrencinin bağlı oldugu kurum bilgisi
    /// </summary>
    public class HospitalAppointmentInstitution : FixEntity<int>, IActivable
    {
        /// <summary>
        /// Kurum Adı
        /// </summary>
        /// <returns></returns>
        public string InstitutionName { get; set; }

        /// <summary>
        /// Kurum Öncelik Sırası
        /// </summary>
        /// <returns></returns>
        public int PriorityOrder { get; set; }

        /// <summary>
        /// Kurum Öğrencileri
        /// </summary>
        /// <returns></returns>
        public ICollection<AppointmentStudent> Students { get; set; }

        public bool IsActive { get; set; }
    }

    public class AppointmentStudent : FixEntity<int>
    {
        /// <summary>
        /// Ad Soyad
        /// </summary>
        /// <returns></returns>
        public string NameSurname { get; set; }

        /// <summary>
        /// Kimlik Numarası
        /// </summary>
        /// <returns></returns>
        public string IdentityNumber { get; set; }

        /// <summary>
        /// MHRS Sifresi
        /// </summary>
        /// <returns></returns>
        public string MHRSPassword { get; set; }

        /// <summary>
        /// İşlem Durumu
        /// </summary>
        /// <returns></returns>
        public ProcessStatus ProcessStatus { get; set; }

        /// <summary>
        /// Tamamlanma Zamanı
        /// </summary>
        /// <returns></returns>
        public DateTime ComplatedTime { get; set; }

        /// <summary>
        /// Öncelik Sırası
        /// </summary>
        /// <returns></returns>
        public int Order { get; set; }

        /// <summary>
        /// Randevu alınabilcek Hastane Bilgisileri
        /// </summary>
        /// <returns></returns>
        public ICollection<AppointmentStudentHospitalRelation> AppointmentStudentHospitalRelations { get; set; }

        /// <summary>
        /// Öğrencinin Bağlı oldugu kurum
        /// </summary>
        /// <returns></returns>
        public HospitalAppointmentInstitution HospitalAppointmentInstitution { get; set; }

    }

    public class AppointmentStudentHospitalRelation : FixEntity<int>
    {
        public AppointmentStudent AppointmentStudent { get; set; }
        public Hospital Hospital { get; set; }
    }


    public enum ProcessStatus
    {
        NotStart = 0,
        Processing = 1,
        Complated = 2
    }
}