using System;

namespace SRM.Services.Api.Reports.Models
{
    public class StudentJoinStatusModeller
    {

    }

    /// <summary>
    /// Öğrenci ders katılım durum raporu
    /// </summary>
    public class StudentJoinStatusRequest
    {
        /// <summary>
        /// Rapor Başlama Tarihi
        /// </summary>
        /// <returns></returns>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Rapor Bitiş Tarihi
        /// </summary>
        /// <returns></returns>
        public DateTime EndDate { get; set; }


    }
}