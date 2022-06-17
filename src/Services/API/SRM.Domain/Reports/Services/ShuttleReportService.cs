using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Shuttles;
using System;
using System.Linq;

namespace SRM.Domain.Reports.Services
{
    public class ShuttleReportService : IShuttleReportService
    {
        private readonly IRepository<ShuttleStudentOperasionLessonRelation> _studentLessonRelationRepository;
        public ShuttleReportService(
            IRepository<ShuttleStudentOperasionLessonRelation> studentLessonRelationRepository)
        {
            _studentLessonRelationRepository = studentLessonRelationRepository;
        }

        /// <summary>
        /// Öğrenci katılım durumu raporları
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public object GetStudentJoinStatus(DateTime startDate, DateTime endDate)
        {

            var list = _studentLessonRelationRepository.Table
                .Include(x => x.ShuttleStudentOperation)
                .ThenInclude(x => x.Student)
                .Where(en =>
                    en.ShuttleStudentOperation.ShuttleOperation.DateTime.Date >= startDate.Date &&
                    en.ShuttleStudentOperation.ShuttleOperation.DateTime.Date <= endDate.Date
                    //&& en.ShuttleStudentOperation.Student.IsActive
                    )
                .GroupBy(en => en.ShuttleStudentOperation.Student)
                .Select(en => new
                {
                    StudentId = en.Key.Id,
                    StudentIdentityNo = en.Key.IdentityNumber,
                    StudentName = en.Key.Name + " " + en.Key.Surname,
                    PlannedLessonCount = en.Sum(u => u.PlannedLessonCount), //Planlanan
                    JoinedLessonCount = en.Sum(u => u.CompletedLessonCount), //Katıldığı
                    NotJoinedLessonCount = en.Sum(u =>
                        u.PlannedLessonCount != 0 ? u.PlannedLessonCount - u.CompletedLessonCount : 0), //Katılmadığı//
                    CompensationLessonCount =
                        en.Sum(u => u.PlannedLessonCount == 0 ? u.CompletedLessonCount : 0),

                }).Select(en => new
                {
                    en.StudentId,
                    en.StudentIdentityNo,
                    en.StudentName,
                    en.PlannedLessonCount, //Planlanan
                    en.JoinedLessonCount, //Katıldığı
                    en.NotJoinedLessonCount, //Katılmadığı//
                    en.CompensationLessonCount, //Telafi .ToList();
                    ParticipationPercentage = (en.PlannedLessonCount + en.CompensationLessonCount) != 0
                        ? Convert.ToDecimal(((en.JoinedLessonCount) / Convert.ToDecimal(en.PlannedLessonCount + en.CompensationLessonCount)) * 100)
                        : (0)

                }).ToList();
            var result = new
            {
                TotalPlannedLessonCount = list.Sum(x => x.PlannedLessonCount),
                TotalJoinedLessonCount = list.Sum(x => x.JoinedLessonCount),
                TotalNotJoinedLessonCount = list.Sum(x => x.NotJoinedLessonCount),
                TotalCompensationLessonCount = list.Sum(x => x.CompensationLessonCount),
                //TotalParticipationPercentage = (list.Sum(x => x.JoinedLessonCount) / (list.Sum(x => x.CompensationLessonCount) + list.Sum(x => x.PlannedLessonCount)) * 100),
                List = list
            };
            return new
            {
                result.TotalPlannedLessonCount,
                result.TotalJoinedLessonCount,
                result.TotalNotJoinedLessonCount,
                result.TotalCompensationLessonCount,
                TotalParticipationPercentage = (result.TotalPlannedLessonCount + result.TotalCompensationLessonCount) != 0
                    ? Convert.ToDecimal(((result.TotalJoinedLessonCount) / Convert.ToDecimal(result.TotalPlannedLessonCount + result.TotalCompensationLessonCount)) * 100)
                    : (0),
                result.List
            };
        }
    }
}