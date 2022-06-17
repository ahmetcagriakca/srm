using Fix;
using SRM.Domain.Dashboard.DTO;
using System;

namespace SRM.Domain.Dashboard.Services
{
    public interface IDashboardService : IDependency
    {
        DailyLessonStatusStatistics GetDailyLessonStatusStatistics(DateTime startDate, DateTime endDate);
        ComingStatusStatistics GetComingStatusStatistics(DateTime startDate, DateTime endDate);
    }
}
