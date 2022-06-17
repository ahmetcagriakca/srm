using Fix.Data;

namespace SRM.Data.Models.CourseManagement
{
    /// <summary>
    /// Derslik
    /// </summary>
    public class ClassRoomTmp : FixEntity<int>
    {
        public string Code { get; set; }
        public string Floor { get; set; }
        public School School { get; set; }
        public int MaxCapacity { get; set; }
    }
}
