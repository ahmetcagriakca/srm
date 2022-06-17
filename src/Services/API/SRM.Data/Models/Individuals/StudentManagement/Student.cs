using Fix.Data;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Data.Models.Shuttles.TemplateManagement;
using System;
using System.Collections.Generic;

namespace SRM.Data.Models.Individuals.StudentManagement
{
    public class Student : Individual
    {
        public Student()
        {
            Addresses = new HashSet<StudentAddress>();
            Lessons = new HashSet<StudentLesson>();
            InstructorRelations = new HashSet<StudentInstructorRelation>();
            Reports = new HashSet<StudentReport>();
            ObstacleTypes = new HashSet<StudentObstacleType>();
        }

        /// <summary>
        /// Doğum tarihi
        /// </summary>
        /// <value>The student birth date.</value>
        public DateTime DateOfBirth { get; set; }

        /// <summary>
        /// Veli Adı
        /// </summary>
        /// <value>The name of the parent.</value>
        public string ParentName { get; set; }

        /// <summary>
        /// Veli Telefon Numarası
        /// </summary>
        /// <value>The parent phone number.</value>
        public string ParentPhoneNumber { get; set; }

        /// <summary>
        /// Kuruma Kayıt Tarihi
        /// </summary>
        /// <value>The course start date.</value>
        public DateTime CourseStartDate { get; set; }

        //TODO Engel tipleri ayrılacak.Liste olarak bağlanacak
        public ICollection<StudentObstacleType> ObstacleTypes { get; set; }

        /// <summary>
        /// Öğrenci Adres Bilgileri
        /// </summary>
        /// <value>The addresses.</value>
        public virtual ICollection<StudentAddress> Addresses { get; set; }

        /// <summary>
        /// Öğrencinin aldığı dersler
        /// </summary>
        /// <value>The lessons.</value>
        public virtual ICollection<StudentLesson> Lessons { get; set; }

        /// <summary>
        /// Öğrenci Öğretmen İlişkilendirmesi
        /// </summary>
        /// <value>The instructor relation.</value>
        public ICollection<StudentInstructorRelation> InstructorRelations { get; set; }

        /// <summary>
        /// Öğrencinin Hastane Raporları
        /// </summary>
        /// <value>The reports.</value>
        public ICollection<StudentReport> Reports { get; set; }

        /// <summary>
        /// Öğrenci Adres Bölgesi
        /// </summary>
        /// <value>The location region.</value>
        public LocationRegion LocationRegion { get; set; }

        //Öğrenci servis taslakları
        public ICollection<ShuttleStudentTemplate> ShuttleStudentTemplates { get; set; }

        /// <summary>
        /// Öğrenci iletişim bilgiler
        /// </summary>
        /// <returns></returns>
        public ICollection<StudentContact> StudentContacts { get; set; }

    }

    /// <summary>
    /// Öğrenci iletişim bilgileri
    /// </summary>
    public class StudentContact : FixEntity<long>
    {
        public Student Student { get; set; }
        ///İletişim ismi
        public string Name { get; set; }

        //İletişim Numarası
        public string Number { get; set; }

    }

}
