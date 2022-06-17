using Fix.Data;
using SRM.Data.Models.Shuttles.Parameters;
using System.Collections.Generic;

namespace SRM.Data.Models.Shuttles.TemplateManagement
{
    public class ShuttleTemplate : FixEntity<int>, IActivable
    {
        public ShuttleTemplate()
        {
            ShuttleStudentTemplates = new HashSet<ShuttleStudentTemplate>();
        }

        /// <summary>
        /// Adres Bölgesi
        /// </summary>
        /// <value>The location region.</value>
        public LocationRegion LocationRegion { get; set; }

        /// <summary>
        /// Haftanın Günü
        /// </summary>
        /// <value>The day of week.</value>
		public int DayOfWeek { get; set; }

        /// <summary>
        /// Saati
        /// </summary>
        /// <value>The hour of date.</value>
		// public int HourOfDate { get; set; }

        //Saat Dakika
        public System.TimeSpan Time { get; set; }

        //Öğrenci servis taslakları
        public ICollection<ShuttleStudentTemplate> ShuttleStudentTemplates { get; set; }

        //Servis aracı
        public StudentService StudentService { get; set; }

        public bool IsActive { get; set; }
    }



}
