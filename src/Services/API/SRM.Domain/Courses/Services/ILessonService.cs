using Fix;
using SRM.Data.Models.Courses;
using System;
using System.Linq;

namespace SRM.Domain.Courses.Services
{
    public interface ILessonService : IDependency
    {
        IQueryable<LessonSession> GetLessonSessions(long id, DateTime? searchDate);

    }
}
