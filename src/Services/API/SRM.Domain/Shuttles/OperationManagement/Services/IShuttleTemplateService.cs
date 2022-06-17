using Fix;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles.TemplateManagement;
using System;
using System.Collections.Generic;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public interface IShuttleTemplateService : IDependency
    {
        ShuttleTemplate GetById(int id);
        //Servis taslağı varmı
        bool IsShuttleTemplateExist(int regionId, int dayOfWeek, TimeSpan hourOfDate);

        //Servis Taslagını Ekle
        void CreateShuttleTemplate(ShuttleTemplate shuttleTemplate, int locationRegionId, int studentServiceId);
        void UpdateShuttleTemplate(ShuttleTemplate entity, int locationRegionId, int studentServiceId);

        //Öğrenci servis taslagı varmı
        void IsStudentTemplateExist(ShuttleTemplate shuttleTemplate, Student student);

        //Öğrenci servis taslagını ekle
        void CreateStudentTemplate(int shuttleTemplateId, long studentId, ShuttleStudentTemplate studentTemplate);

        //Öğrenci servis taslagını günceller
        void UpdateStudentTemplate(long studentTemplateId, int order, int lessonCount);

        //Servis Taslagını sil
        void DeleteShuttleTemplate(int shuttleTemplateId);

        //Öğrenci Taslagını sil
        void DeleteStudentTemplate(long studentTemplateId);

        //Bütün servis taslaklarını getir
        IEnumerable<ShuttleTemplate> GetAllShuttleTemplate();

        //Günlük servis taslaklarını getir
        IEnumerable<ShuttleTemplate> GetShuttleTemplateByDayOfWeek(int? dayOfWeek);

        //Servis taslagının öğrenci taslaklarını getir
        IEnumerable<ShuttleStudentTemplate> GetStudentTemplateByShuttleTemplateId(int shuttleTemplateId);

        //Öğrenci taslagını id ye göre getir
        ShuttleStudentTemplate GetStudentTemplateById(int studentTemplateId);

        /// <summary>
        /// Öğrenci taslaklarını silme
        /// </summary>
        /// <param name="student"></param>
        void DeleteStudentTemplateByStudentId(long studentId);

        //Bölgenin ilişkili oldugu alt bölge tanımı kaldırılken taslaklarda alt bölgeden gelen öğrenci varsa sildirmeyizß
        void CheckDeleteSubRegion(int regionId, int subRegionId);
    }
}