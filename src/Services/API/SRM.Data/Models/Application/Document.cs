using Fix.Data;
using System;

namespace SRM.Data.Models.Application
{
    public class Document : FixEntity<Guid>
    {
        /// <summary>
        /// Döküman Başlığı
        /// </summary>
        /// <value>The name.</value>
        public string Title { get; set; }

        /// <summary>
        /// Döküman Adı
        /// </summary>
        /// <value>The name of the file.</value>
        public string FileName { get; set; }

        /// <summary>
        /// Döküman Tipi
        /// </summary>
        /// <value>The type of the file.</value>
        public string FileType { get; set; }

        /// <summary>
        /// Döküman Adresi
        /// </summary>
        /// <value>The path.</value>
        public string ContentPath { get; set; }

    }
}
