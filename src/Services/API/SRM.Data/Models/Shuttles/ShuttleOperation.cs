using Fix.Data;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Data.Models.Shuttles.TemplateManagement;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Shuttles
{
    public class ShuttleOperation : FixEntity<long>
    {
        public ShuttleOperation()
        {
            ShuttleStudentOperations = new HashSet<ShuttleStudentOperation>();

        }
        /// <summary>
        /// Servis Taslağı
        /// </summary>
        /// <value>The shuttle template.</value>
        public ShuttleTemplate ShuttleTemplate { get; set; }

        /// <summary>
        /// Gün Ve Saati
        /// </summary>
        /// <value>The date time.</value>
		public DateTime DateTime { get; set; }

        //Operasyon Durumu
        public ShuttleOperationStatus ShuttleOperationStatus { get; set; }

        //Servisin göreve başlama zamanı
        public DateTime? OperationStartTime { get; set; }

        //Servisin görevi bitirme / okula geliş zamanı
        public DateTime? OperationEndTime { get; set; }

        /// <summary>
        /// Adres Bölgesi
        /// </summary>
        /// <value>The location region.</value>
        public LocationRegion LocationRegion { get; set; }

        /// <summary>
        /// Servisin Öğrenci Listesi
        /// </summary>
        public ICollection<ShuttleStudentOperation> ShuttleStudentOperations { get; set; }

        //Servis aracı
        public StudentService StudentService { get; set; }
    }
    public enum ShuttleOperationStatus
    {
        //Başlamadı
        Waiting = 0,
        //Operasyon esnasında
        InOperation = 1,

        //operasyon bitti
        OprationFinished = 2

    }
}
