using Fix;
using System;

namespace SRM.Domain.Reports.Services
{
    public interface IShuttleReportService : IDependency
    {
        /// <summary>
        /// Öğrenci ders katılım durum raporu
        /// </summary>
        /// <param name="startDate">Başalama tarihi</param>
        /// <param name="endDate">Bitiş tarihi</param>
        object GetStudentJoinStatus(DateTime startDate, DateTime endDate);
    }

}