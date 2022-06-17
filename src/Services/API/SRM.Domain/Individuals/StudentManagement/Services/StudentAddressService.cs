using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentAddressService : IStudentAddressService
    {
        private readonly IRepository<StudentAddress> studentAddressRepository;
        private readonly IRepository<LocationRegion> studentLocationRegionRepository;
        private readonly IStudentService studentService;

        public StudentAddressService(
            IRepository<StudentAddress> studentAddressRepository,
            IRepository<LocationRegion> studentLocationRegionRepository,
            IStudentService studentService
            )
        {
            this.studentAddressRepository = studentAddressRepository ?? throw new ArgumentNullException(nameof(studentAddressRepository));
            this.studentLocationRegionRepository = studentLocationRegionRepository;
            this.studentService = studentService;
        }
        #region Student Address

        private IQueryable<StudentAddress> GetWithRelations()
        {
            var entities = studentAddressRepository.Table
                .Include(en => en.Student)
                .Include(en => en.Address)
                .ThenInclude(en => en.LocationRegion)
                .Include(en => en.Address.Location);
            return entities;
        }

        public IEnumerable<StudentAddress> Get(long studentId)
        {
            var results = GetWithRelations()
                .Where(en => en.Student.Id == studentId);
            return results;
        }

        public StudentAddress GetById(long studentId, long id)
        {
            var result = GetWithRelations()
                .FirstOrDefault(en => en.Student.Id == studentId && en.Id == id);
            return result;
        }

        public void Create(long studentId, StudentAddress entity, int locationRegionId)
        {
            var student = studentService.GetStudentById(studentId);
            entity.Student = student;
            entity.Address.LocationRegion = studentLocationRegionRepository.FindBy(locationRegionId);

            studentAddressRepository.Add(entity);
        }

        public void Update(long studentId, StudentAddress entity, int locationRegionId)
        {
            var student = studentService.GetStudentById(studentId);
            entity.Student = student;
            entity.Address.LocationRegion = studentLocationRegionRepository.FindBy(locationRegionId);
            studentAddressRepository.Update(entity);

            studentService.UpdateStudentLocationRegion(studentId, locationRegionId);
        }

        public void Delete(long studentId, long id)
        {
            var entity = GetById(studentId, id);
            studentAddressRepository.Delete(entity);
        }

        #endregion
    }
}
