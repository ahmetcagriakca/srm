using Fix.Data;
using System.Collections.Generic;

namespace SRM.Data.Models.Courses.Parameters
{
    public class ClassRoom : ParameterEntity<int>
    {
        /// <summary>
        /// Derslik Adı
        /// </summary>
        /// <value>The name.</value>
        public string Name { get; set; }

        /// <summary>
        /// Mebbis derslik tanım numarası
        /// </summary>
        /// <value>The mebbis no.</value>
        public string MebbisNo { get; set; }

        public ICollection<ClassRoomAvailableBranch> AvailableBranchs { get; set; }

        /// <summary>
        /// Derslik Bireysel mi grup dersliği mi
        /// </summary>
        /// <value><c>true</c> if is personel; otherwise, <c>false</c>.</value>
        public bool IsPersonel { get; set; }
    }

}
