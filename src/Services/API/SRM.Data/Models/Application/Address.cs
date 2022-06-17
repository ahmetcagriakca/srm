using Fix.Data;
// using GeoAPI.Geometries;
// using NetTopologySuite.Geometries;
using SRM.Data.Models.Shuttles.Parameters;

namespace SRM.Data.Models.Application
{
    public class Address : FixEntity<long>
    {
        /// <summary>
        /// Adres Başlığı
        /// </summary>
        /// <value>The title.</value>
        public string Title { get; set; }

        /// <summary>
        /// Açık Adres Bilgisi
        /// </summary>
        /// <value>The adress.</value>
        public string AddressInfo { get; set; }

        /// <summary>
        /// Semt
        /// </summary>
        /// <value>The neighborhood.</value>
        public Neighborhood Neighborhood { get; set; }

        /// <summary>
        /// Adresleri gruplamak icin kullanıcak alan 
        /// TODO adres bölgesi içeriği genişletilebilir
        /// </summary>
        /// <value>The area.</value>
        public string Area { get; set; }

        /// <summary>
        /// Adres Tarifi
        /// </summary>
        /// <value>The adress directions.</value>
		public string AddressDirections { get; set; }

        /// <summary>
        /// Adres Önceliği
        /// </summary>
        /// <value>The priority.</value>
        public int Priority { get; set; }

        /// <summary>
		/// koordinat bilgisi
        /// </summary>
        /// <value>The location.</value>
        public Location Location { get; set; }

        //Adres bölgesi
        public LocationRegion LocationRegion { get; set; }
    }

    /// <summary>
    /// Şehir
    /// </summary>
	public class City : FixEntity<int>
    {
        /// <summary>
        /// Plaka Kodu
        /// </summary>
        /// <value>The plate code.</value>
        public int PlateCode { get; set; }

        /// <summary>
        /// Şehir Adı
        /// </summary>
        /// <value>The name.</value>
		public string Name { get; set; }
    }

    /// <summary>
    /// ilçe
    /// </summary>
	public class County : FixEntity<int>
    {
        /// <summary>
        /// İlçe Adı
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Şehir
        /// </summary>
        /// <value>The city.</value>
		public City City { get; set; }

    }

    /// <summary>
    /// Semt
    /// </summary>
	public class Neighborhood : FixEntity<int>
    {
        /// <summary>
        /// Semt Adı
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// İlçe
        /// </summary>
        /// <value>The county.</value>
		public County County { get; set; }
    }

    public class Location : FixEntity<long>
    {
        /// <summary>
        /// Enlem
        /// </summary>
        /// <value>The latitude.</value>
        public string Latitude { get; set; }

        /// <summary>
        /// Boylam
        /// </summary>
        /// <value>The longitude.</value>
		public string Longitude { get; set; }

        // [Column(TypeName = "geography")]
        // public Point LocationPoint { get; set; }

        public double LocationX { get; set; }
        public double LocationY { get; set; }

    }
}
