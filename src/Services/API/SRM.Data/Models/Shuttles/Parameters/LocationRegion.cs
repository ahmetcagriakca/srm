using Fix.Data;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;

namespace SRM.Data.Models.Shuttles.Parameters
{
    public class LocationRegion : ParameterEntity<int>
    {
        public LocationRegion()
        {
            RegionRelations = new HashSet<LocationRegionRelation>();
            Students = new HashSet<Student>();
        }
        /// <summary>
        /// Bölge Adı
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Bölge Kodu
        /// </summary>
        /// <value>The code.</value>
        public int Code { get; set; }

        /// <summary>
        /// İlişkili gidilebilir bölgeler
        /// </summary>
        /// <value>The region relations.</value>
        public ICollection<LocationRegionRelation> RegionRelations { get; set; }

        /// <summary>
        /// Bölgedeki öğrenciler
        /// </summary>
        /// <value>The students.</value>
        public ICollection<Student> Students { get; set; }
    }
}
