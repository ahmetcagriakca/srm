using Fix.Data;

namespace SRM.Data.Models.Courses.Parameters
{
    /// <summary>
    /// Ders Bransı
    /// </summary>
    public class Branch : ParameterEntity<int>
    {
        /// <summary>
        /// Branş Adı
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Branş Açıklması
        /// </summary>
        /// <value>The description.</value>
		public string Description { get; set; }
    }
}
