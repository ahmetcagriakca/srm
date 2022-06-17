using Fix;
using SRM.Data.Models.CallManagement;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles;
using System;
using System.Collections.Generic;

namespace SRM.Domain.Shuttles.OperationManagement.Services
{
    public interface IShuttleService : IDependency
    {
        /// <summary>
        /// Haftalık servis güzergahlarını oluşturma
        /// </summary>
        /// <returns><c>true</c>, if weekly shuttle operation was created, <c>false</c> otherwise.</returns>
        /// <param name="WeekStartDate">week start date</param>
        bool CreateWeeklyShuttleOperation(DateTime weekStartDate);

        /// <summary>
        /// Custom operasyon oluşturma
        /// </summary>
        /// <param name="shuttleOperationTemplateId"></param>
        /// <returns></returns>
        bool CreateShuttleOperationByTemplateId(long shuttleOperationTemplateId);

        /// <summary>
        /// Öğrenci servis operasyon durumunu günceller  öğrenci servise  bindi binmedi set edilir
        /// </summary>
        /// <param name="shuttleStudentOperation"></param>
        /// <returns></returns>
        void SetStudentShuttleOperationStatus(long shuttleStudentOperationId, ShuttleStudentOperationStatus ComingStatus);

        /// <summary>
        /// servi detay bilgileri
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        ShuttleOperation GetStudentShuttleOperationById(int id);

        /// <summary>
        /// Gün bazında öğrenci servis operasyon listesini doner
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<ShuttleOperation> GetStudentOperationListByDate(DateTime date);

        //Servis operasyon öğrencilerini getir
        IEnumerable<ShuttleStudentOperation> GetStudentOperationListByShuttleOperationId(long serviceOperationId);

        /// <summary>
        /// Operasyon detay bilgilerini getirir
        /// </summary>
        /// <param name="operationId"></param>
        /// <returns></returns>
        ShuttleOperation GetShuttleOperationById(int operationId);

        /// <summary>
        /// Gün bazında servis operasyon listesini doner
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<ShuttleOperation> GetShuttleOperationListByDate(DateTime date);

        /// <summary>
        /// Söförler icin servis listesi
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<ShuttleOperation> GetShuttleOperationListByDateForDriver(DateTime date);

        /// <summary>
        /// Öğrenci arama öneri listesi günlük
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByDate(DateTime date);

        /// <summary>
        /// Öğrenci arama öneri listesi servis operasyonu bazında
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByShuttleOperationId(long serviceOperationId);

        /// <summary>
        /// Öğrenciye ait o günlük servis önerileri
        /// </summary>
        IEnumerable<ShuttleStudentOperationAdvice> GetStudentOperationAdvicesByDateAndStudent(DateTime date, long studentId);

        /// <summary>
        /// Önerinin tüm bilgileri
        /// </summary>
        /// <param name="adviceId"></param>
        /// <returns></returns>
        ShuttleStudentOperationAdvice GetStudentShuttleAdviceById(int adviceId);

        /// <summary>
        /// Öğrenci Önerisini operasyona dönüştürür
        /// </summary>
        /// <param name="studentId"></param>
        /// <param name="studentShuttleOperationId"></param>
        /// <param name="answer"></param>
        ShuttleStudentOperation SetAdviceToOperation(long operationId, long studentId, string description);

        //Servis öğrenci öneri listesini oluşturu
        void CreateAdvice(DateTime date, long shuttleOpID);

        //lokasyon bilgisi kaydetme
        void SetStudentOperationLocation(long shuttleStudentOperation, string locationX, string locationY);

        //Servis Operasyon işlem durumunu set eder.
        void SetShuttleOperationStatus(long shuttleOparationId, ShuttleOperationStatus status);

        //Öğrenci uygunluk durumuna göre operasyon ve önerileri günceller
        void ChangeStudentOperationByAvaibleTime(long studentAvailableTimeId);

        //Manuel Servis Operasyonu oluşturma
        long CreateCustomShuttleOperation(DateTime date, int regionId, int studentServiceId);

        //Manuel Öğrenci Operasyonu oluşturma
        void CreateCustomStudentOperation(long studentId, long shuttleOperationId, int lessonCount);

        //İlişkiden kaldırılan bölgeler icin önerileri silme
        void DeleteSubRegionAdvice(int regionId, int subRegionId);

        //Öğrenci katıldıgı ders sayısı set etme servisi
        void SetStudentOperastionLessonsCount(long shuttleStudentOperationId, int completedLessonCount);

        /// <summary>
        /// Mobil Söför icin öğrenci durumunu set etme metodu
        /// </summary>
        /// <param name="shuttleStudentOperationId"> öğrenci operasyon id</param>
        /// <param name="ComingStatus"> katılım durumu</param>
        /// <param name="locationX">location x</param>
        /// <param name="locationY">location y</param>
        void SetStudentShuttleOperationStatusForDriver(long shuttleStudentOperationId, ShuttleStudentOperationStatus comingStatus, string locationX, string locationY);

        //Öğretmen günlük öprenci listesi
        IEnumerable<ShuttleStudentOperation> GetInstructorStudents(DateTime date);

        IEnumerable<ShuttleStudentOperation> GetStudentOperations(long studentId);

        IEnumerable<ShuttleStudentOperation> GetShuttleOperationStudentLocations(long shuttleOperationId);

        ShuttleStudentOperation GetShuttleStudentOperationByStudent(long operationId, long studentId);
    }
}