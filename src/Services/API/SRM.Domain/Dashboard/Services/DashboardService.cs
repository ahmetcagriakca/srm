using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Shuttles;
using SRM.Domain.Dashboard.DTO;
using System;
using System.Linq;

namespace SRM.Domain.Dashboard.Services
{
    public class DashboardService : IDashboardService
    {
        private readonly IRepository<ShuttleStudentOperasionLessonRelation> _shuttleStudentOperasionLessonRelationRepository;

        public DashboardService(
            IRepository<ShuttleStudentOperasionLessonRelation> shuttleStudentOperasionLessonRelationRepository
            )
        {
            _shuttleStudentOperasionLessonRelationRepository = shuttleStudentOperasionLessonRelationRepository;
        }

        /// <summary>
        /// Verilen güne ait servisteki planlanan ders sayısı üretilen öneri sayısının gösterilmesi.
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public DailyLessonStatusStatistics GetDailyLessonStatusStatistics(DateTime startDate, DateTime endDate)
        {
            var query = _shuttleStudentOperasionLessonRelationRepository.Table
                .Include(x => x.ShuttleStudentOperation).ThenInclude(x => x.Student)
                .Include(x => x.ShuttleStudentOperation).ThenInclude(en => en.ShuttleOperation);

            var queryWhere = query.Where(x =>
             x.ShuttleStudentOperation.ShuttleOperation.DateTime.Date >= startDate.Date);
            if (endDate != default)
            {
                queryWhere = queryWhere.Where(x =>
                    x.ShuttleStudentOperation.ShuttleOperation.DateTime.Date <= endDate.Date);
            }
            var queryGroup = queryWhere.GroupBy(x => x.ShuttleStudentOperation.Student)
             .Select(z => new
             {
                 PlannedLessonCount = z.Sum(u => u.PlannedLessonCount),//Planlanan
                 CompensationLessonCount = z.Sum(u => u.PlannedLessonCount == 0 ? u.CompletedLessonCount : 0)//Telafi 

             }).ToList();
            var statistics = new DailyLessonStatusStatistics
            {
                PlannedLessonCount = queryGroup.Sum(x => x.PlannedLessonCount),
                CompensationLessonCount = queryGroup.Sum(x => x.CompensationLessonCount),
            };
            statistics.TotalCount = statistics.PlannedLessonCount + statistics.CompensationLessonCount;
            return statistics;
        }

        /// <summary>
        /// Verilen güne ait servise gelen gelmeyen öğrenci durumu
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public ComingStatusStatistics GetComingStatusStatistics(DateTime startDate, DateTime endDate)
        {

            var query = _shuttleStudentOperasionLessonRelationRepository.Table
                .Include(x => x.ShuttleStudentOperation).ThenInclude(x => x.Student)
                .Include(x => x.ShuttleStudentOperation).ThenInclude(en => en.ShuttleOperation);

            var queryWhere = query.Where(x =>
                x.ShuttleStudentOperation.ShuttleOperation.DateTime.Date >= startDate.Date);
            if (endDate != default)
            {
                queryWhere = queryWhere.Where(x =>
                    x.ShuttleStudentOperation.ShuttleOperation.DateTime.Date <= endDate.Date);
            }

            var queryGroup = queryWhere.GroupBy(x => x.ShuttleStudentOperation.Student)
                .Select(z => new
                {
                    JoinedLessonCount = z.Sum(u => u.CompletedLessonCount),//Katıldığı
                    NotJoinedLessonCount = z.Sum(u => u.PlannedLessonCount != 0 ? u.PlannedLessonCount - u.CompletedLessonCount : 0),//Katılmadığı//
                }).ToList();
            var statistics = new ComingStatusStatistics
            {
                ComingLessonCount = queryGroup.Sum(x => x.JoinedLessonCount),
                NotComingLessonCount = queryGroup.Sum(x => x.NotJoinedLessonCount),
            };
            statistics.TotalCount = statistics.ComingLessonCount + statistics.NotComingLessonCount;

            return statistics;
        }
    }
}
