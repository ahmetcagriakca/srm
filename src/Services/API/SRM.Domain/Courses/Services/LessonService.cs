using Fix.Data;
using SRM.Data.Models.Courses;
using System;
using System.Linq;

namespace SRM.Domain.Courses.Services
{
    public class LessonService : ILessonService
    {
        private readonly IRepository<Lesson> _lessonRepository;
        private readonly IRepository<LessonSession> _lessonSessionRepository;
        public LessonService(IRepository<Lesson> lessonRepository, IRepository<LessonSession> lessonSessionRepository)
        {
            _lessonRepository = lessonRepository;
            _lessonSessionRepository = lessonSessionRepository;
        }

        public IQueryable<LessonSession> GetLessonSessions(long lessonId, DateTime? searchDate)
        {
            return null;
            //return _lessonSessionRepository.Table.Include(en=>en.Lesson).ThenInclude(en=>en.Student).Include(en => en.Instructor).Where(en => en.Lesson.Id == lessonId && (searchDate == null || en.StartDate <= searchDate));
        }
    }
}
