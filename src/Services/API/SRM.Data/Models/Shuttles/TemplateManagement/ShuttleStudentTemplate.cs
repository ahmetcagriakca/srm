using Fix.Data;
using SRM.Data.Models.Individuals.StudentManagement;

namespace SRM.Data.Models.Shuttles.TemplateManagement
{
    //Öğrenci servis taslağı
    public class ShuttleStudentTemplate : FixEntity<long>, IActivable
    {
        //Servis taslağı
        public ShuttleTemplate ShuttleTemplate { get; set; }

        //Öğrenci
        public Student Student { get; set; }

        //Servis Biniş sırası
        public int Order { get; set; }

        //Ders Sayısı
        public int LessonCount { get; set; }

        public bool IsActive { get; set; }
    }
}