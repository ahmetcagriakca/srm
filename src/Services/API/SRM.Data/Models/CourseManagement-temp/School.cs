using Fix.Data;
using System.Collections.Generic;

namespace SRM.Data.Models.CourseManagement
{
    public class School : FixEntity<int>
    {
        public string Name { get; set; }
        public ICollection<ClassRoomTmp> ClassRooms { get; set; }
    }
}
