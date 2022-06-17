using Fix.Data;

namespace SRM.Data.Models.Courses.Parameters
{
    /// <summary>
    /// Derslikte yapılacak derslerin uygun branş tanımları
    /// Derslik Brans ilişkilendirmesi "1 to n"
    /// </summary>
    public class ClassRoomAvailableBranch : ParameterEntity<int>
    {
        public ClassRoom ClassRoom { get; set; }
        public Branch Branch { get; set; }
    }
}
