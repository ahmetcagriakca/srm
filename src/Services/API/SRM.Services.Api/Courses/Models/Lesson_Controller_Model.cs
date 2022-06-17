using SRM.Data.Models.Courses;
using SRM.Services.Api.BaseModel;
using System;
using System.Linq;

namespace SRM.Services.Api.Courses.Models
{

    public class SearchStudentLessonSessionsRequest
    {
        public long Id { get; set; }
        public DateTime? SearchDate { get; set; }

    }

    public class SearchStudentLessonSessionsResponse
    {
        public long Id { get; set; }
        public DateTime? SearchDate { get; set; }

    }

    public static class LessonModeller
    {
        public static object ToLessonSessionsResponse(IQueryable<LessonSession> lesssonSessions)
        {
            var entities = from s in lesssonSessions
                           select new
                           {
                               s.Id,
                               s.Instructor,
                               Lesson = new
                               {
                                   s.Lesson.Name,
                               },
                               s.Header,
                               s.Content,
                               s.StartDate
                           };
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

    }
}
