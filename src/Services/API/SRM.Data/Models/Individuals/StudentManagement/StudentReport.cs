using Fix.Data;
using SRM.Data.Models.Individuals.Parameters;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class StudentReport : FixEntity<long>, IActivable
    {
        /// <summary>
        /// Rapor No
        /// </summary>
        /// <value>The report number.</value>
        public string ReportNumber { get; set; }

        /// <summary>
        /// Raporu veren hastane
        /// </summary>
        /// <value>The given hospital.</value>
        public Hospital GivenHospital { get; set; }

        /// <summary>
        /// Rapor Başlama Tarihi
        /// </summary>
        /// <value>The start date.</value>
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Rapor Bitiş Tarihi
        /// </summary>
        /// <value>The end date.</value>
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Açıklama
        /// </summary>
        /// <value>The Description.</value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the content.
        /// </summary>
        /// <value>The content.</value>
        public string Content { get; set; }

        /// <summary>
        /// Öğrenci
        /// </summary>
        /// <value>The student.</value>
        public Student Student { get; set; }

        /// <summary>
        /// Rapor Dökümanları
        /// </summary>
        /// <value>The documents.</value>
        public ICollection<StudentReportDocument> Documents { get; set; }

        public bool IsActive { get; set; }
    }
}
