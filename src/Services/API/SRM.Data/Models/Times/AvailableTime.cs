using Fix.Data;
using System;

namespace SRM.Data.Models.Times
{
    public abstract class AvailableTime : FixEntity<long>
    {
        //Uygunluk durumu
        public bool IsAvaible { get; set; }

        //Sürekli mi parçalı mı
        public bool IsIntegrated { get; set; }

        /// <summary>
        /// Başlama Tarihi
        /// </summary>
        /// <value>The begin time.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Bitiş Tarihi
        /// </summary>
        /// <value>The end time.</value>
        public DateTime EndDate { get; set; }

        //Entegre olmayan kayıtlar icin başlama saati
        public DateTime? StartTime { get; set; }

        //Entegre olmayan kayıtlar icin bitiş saati
        public DateTime? EndTime { get; set; }

        //İlgili günler kombinasyonu
        public DateCombination IncludedDate { get; set; }

        //Açıklama
        public string Description { get; set; }
    }
}
