using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentContactService : IStudentContactService
    {
        private readonly IRepository<StudentContact> studentContactRepository;
        private readonly IStudentService studentService;

        public StudentContactService(IRepository<StudentContact> _studentContactRepository,
        IStudentService _studentService)
        {
            studentContactRepository = _studentContactRepository;
            studentService = _studentService;

        }
        private IQueryable<StudentContact> GetWithRelations()
        {
            var entities = studentContactRepository.Table
                .Include(en => en.Student);
            return entities;
        }
        public IEnumerable<StudentContact> Get(long studentId)
        {
            return GetWithRelations().Where(x => x.Student.Id == studentId);

        }

        public StudentContact GetById(long studentId, long id)
        {
            return GetWithRelations().FirstOrDefault(x => x.Student.Id == studentId && x.Id == id);
        }

        public void Create(long studentId, StudentContact entity)
        {
            var student = studentService.GetStudentById(studentId);
            entity.Student = student;

            studentContactRepository.Add(entity);
        }

        public void Delete(long studentId, long id)
        {
            var entity = GetById(studentId, id);
            studentContactRepository.Delete(entity);
        }

        public void Update(long studentId, StudentContact entity)
        {
            var student = studentService.GetStudentById(studentId);
            entity.Student = student;
            studentContactRepository.Update(entity);
        }
    }
}