using Fix.Data;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Times;
using SRM.Domain.Shuttles.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Domain.Individuals.StudentManagement.Services
{
    public class StudentAvailableTimeService : IStudentAvailableTimeService
    {
        private readonly IRepository<StudentAvailableTime> _studentAvailableTimeRepository;
        private readonly IRepository<DateCombination> dateCombinationRepository;

        public StudentAvailableTimeService(IRepository<StudentAvailableTime> studentAvailableTimeRepository,
            IRepository<DateCombination> dateCombinationRepository
            )
        {
            _studentAvailableTimeRepository = studentAvailableTimeRepository;
            this.dateCombinationRepository = dateCombinationRepository;
        }

        #region StudentAvailableTimes

        public IEnumerable<StudentAvailableTime> Get(long studentId)
        {
            var results = _studentAvailableTimeRepository.Table
                .Include(en => en.Student)
                .Include(en => en.IncludedDate)
                .Where(en => en.Student.Id == studentId)/*.Where(en=> (searchDate == null || en.LessonSessions.Any(lsEn => lsEn.StartDate < searchDate) || !en.LessonSessions.Any()))*/;
            return results;
        }

        public StudentAvailableTime GetById(long studentId, long id)
        {
            var result = _studentAvailableTimeRepository.Table
                .Include(en => en.Student)
                .Include(en => en.IncludedDate)
                .FirstOrDefault(en => en.Student.Id == studentId && en.Id == id);
            return result;
        }

        private bool CheckIncludedDate(DateCombination entity, DateCombination item)
        {
            return entity.Monday == item.Monday ||
                    entity.Tuesday == item.Tuesday ||
                    entity.Wednesday == item.Wednesday ||
                    entity.Thursday == item.Thursday ||
                    entity.Friday == item.Friday ||
                    entity.Saturday == item.Saturday ||
                    entity.Sunday == item.Sunday;
        }
        private bool CheckAvailableTime(StudentAvailableTime entity, StudentAvailableTime item)
        {
            if (entity.StartTime.Value.TimeOfDay > item.StartTime.Value.TimeOfDay && entity.StartTime.Value.TimeOfDay < item.EndTime.Value.TimeOfDay)
            {
                return false;
            }
            else if (entity.EndTime.Value.TimeOfDay > item.StartTime.Value.TimeOfDay && entity.EndTime.Value.TimeOfDay < item.EndTime.Value.TimeOfDay)
            {
                return false;
            }
            if (item.StartTime.Value.TimeOfDay > entity.StartTime.Value.TimeOfDay && item.StartTime.Value.TimeOfDay < entity.EndTime.Value.TimeOfDay)
            {
                return false;
            }
            else if (item.EndTime.Value.TimeOfDay > entity.StartTime.Value.TimeOfDay && item.EndTime.Value.TimeOfDay < entity.EndTime.Value.TimeOfDay)
            {
                return false;
            }
            return true;
        }
        private bool CheckStudentAvailableTime(StudentAvailableTime entity, out string message)
        {
            message = string.Empty;
            if (entity.StartDate > entity.EndDate)
            {
                message = $"Başlangıç tarihi bitiş tarihinden büyük olamaz. ";
                return false;
            }
            if (entity.IsIntegrated)
            {
                if (entity.StartTime > entity.EndTime)
                {
                    message = $"Başlangıç saati bitiş saatinden büyük olamaz. ";
                    return false;
                }
            }
            var availableTimes = _studentAvailableTimeRepository.Table.Include(en => en.IncludedDate).Where(en => en.Student == entity.Student && en.IsIntegrated == entity.IsIntegrated && en.Id != entity.Id);
            foreach (var item in availableTimes)
            {
                if (item.IsIntegrated)
                {
                    if (entity.StartDate > item.StartDate && entity.StartDate < item.EndDate)
                    {
                        message = $"Başlangıç tarih aralığına kapsayan kısıt bulunuyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                        return false;
                    }
                    else if (entity.EndDate > item.StartDate && entity.EndDate < item.EndDate)
                    {
                        message = $"Bitiş tarih aralığına kapsayan kısıt bulunuyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                        return false;
                    }
                    if (item.StartDate > entity.StartDate && item.StartDate < entity.EndDate)
                    {
                        message = $"Kısıt başka bir kısıtın tarih aralığını kapsıyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()} ";
                        return false;
                    }
                    else if (item.EndDate > entity.StartDate && item.EndDate < entity.EndDate)
                    {
                        message = $"Kısıt başka bir kısıtın tarih aralığını kapsıyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                        return false;
                    }
                }
                else if (!item.IsIntegrated)
                {
                    if (entity.StartDate > item.StartDate && entity.StartDate < item.EndDate && CheckIncludedDate(entity.IncludedDate, item.IncludedDate))
                    {
                        if (!CheckAvailableTime(entity, item))
                        {
                            message = $"Başlangıç tarih aralığına kapsayan kısıt bulunuyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                            return false;
                        }
                    }
                    else if (entity.EndDate > item.StartDate && entity.EndDate < item.EndDate && CheckIncludedDate(entity.IncludedDate, item.IncludedDate))
                    {
                        if (!CheckAvailableTime(entity, item))
                        {
                            message = $"Bitiş tarih aralığına kapsayan kısıt bulunuyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                            return false;
                        }
                    }
                    if (item.StartDate > entity.StartDate && item.StartDate < entity.EndDate && CheckIncludedDate(entity.IncludedDate, item.IncludedDate))
                    {
                        if (!CheckAvailableTime(entity, item))
                        {
                            message = $"Kısıt başka bir kısıtın tarih aralığını kapsıyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()} ";
                            return false;
                        }
                    }
                    else if (item.EndDate > entity.StartDate && item.EndDate < entity.EndDate && CheckIncludedDate(entity.IncludedDate, item.IncludedDate))
                    {
                        if (!CheckAvailableTime(entity, item))
                        {
                            message = $"Kısıt başka bir kısıtın tarih aralığını kapsıyor. Girilen tarih:{entity.StartDate.ToShortDateString()}-{entity.EndDate.ToShortDateString()} Mevcut tarih:{item.StartDate.ToShortDateString()}-{item.EndDate.ToShortDateString()}";
                            return false;
                        }
                    }
                }
            }
            return true;
        }

        public void Create(StudentAvailableTime entity)
        {
            if (entity.IsIntegrated)
            {
                entity.IncludedDate = null;
                entity.StartTime = null;
                entity.EndTime = null;
            }
            if (!CheckStudentAvailableTime(entity, out string message))
            {
                throw new StudentAvailableTimeExistException(message);
            }
            _studentAvailableTimeRepository.Add(entity);
        }

        public void Update(StudentAvailableTime entity)
        {
            if (entity.IsIntegrated)
            {
                entity.IncludedDate = null;
                entity.StartTime = null;
                entity.EndTime = null;
            }
            if (!CheckStudentAvailableTime(entity, out string message))
            {
                throw new StudentAvailableTimeExistException(message);
            }
            _studentAvailableTimeRepository.Update(entity);
        }

        public void Delete(long studentId, long id)
        {
            var entity = GetById(studentId, id);
            _studentAvailableTimeRepository.Delete(entity);
        }
        #endregion
    }
}
