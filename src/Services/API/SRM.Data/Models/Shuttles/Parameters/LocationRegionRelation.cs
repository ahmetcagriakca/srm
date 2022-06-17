using Fix.Data;

namespace SRM.Data.Models.Shuttles.Parameters
{
    /// <summary>
    /// Servis bölgeleri ilişkileri
    /// </summary>
    public class LocationRegionRelation : FixEntity<int>
    {
        /// <summary>
        /// Ana Bölge
        /// </summary>
        /// <value>The main region.</value>
        public LocationRegion MainRegion { get; set; }

        /// <summary>
        /// İlişkili bölge
        /// </summary>
        /// <value>The sub region.</value>
        public LocationRegion SubRegion { get; set; }

    }
}
