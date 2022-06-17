using FluentValidation;
using SRM.Data.Models.Shuttles;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Shuttles.OperationManagement.Models
{
    //TODO:validator
    //[Validator(typeof(SetStudentShuttleOperationStatusRequestValidator))]

    public class SetStudentShuttleOperationStatusRequest
    {
        public long StudentShuttleOperationId { get; set; }
        public ShuttleStudentOperationStatus StudentOperasionStatus { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }
    }

    public class SetStudentShuttleOperationAdviceRequest
    {
        public long OperationId { get; set; }
        public long StudentId { get; set; }
        public bool Answer { get; set; }
        public string Description { get; set; }
    }

    public class SetStudentShuttleOperationStatusRequestValidator : AbstractValidator<SetStudentShuttleOperationStatusRequest>
    {
        public SetStudentShuttleOperationStatusRequestValidator()
        {
            RuleFor(x => x.StudentShuttleOperationId).NotNull().NotEmpty().WithMessage("Öğrenci Operasyon Bilgisi boş  geçilemez");

        }

    }

    public static class ShuttleModeller
    {
        internal static object GetStudentShuttleAdvice(this ShuttleStudentOperationAdvice shuttleOperationAdvices)
        {
            var entities = new
            {
                adviceId = shuttleOperationAdvices.Id,
                shuttleOperationId = shuttleOperationAdvices.ShuttleOperation.Id,
                DateTime = shuttleOperationAdvices.ShuttleOperation.DateTime,
                StudentId = shuttleOperationAdvices.Student.Id,
                StudentName = shuttleOperationAdvices.Student.Name + " " + shuttleOperationAdvices.Student.Surname,
                StudentDiscontinuityCount = shuttleOperationAdvices.DisContinuityCount,
                StudentMounthlyDiscontinuityCount = shuttleOperationAdvices.MounthlyDiscontinuityCount,
                ParentName = shuttleOperationAdvices.Student.ParentName,
                Phone = shuttleOperationAdvices.Student.ParentPhoneNumber,
                DestinationName = shuttleOperationAdvices.ShuttleOperation.LocationRegion.Name
            };
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetStudentShutleCallListResponse(this IEnumerable<ShuttleStudentOperationAdvice> shuttleOperationAdvices)
        {
            var entities = (from so in shuttleOperationAdvices.OrderBy(x => x.ShuttleOperation.DateTime).ThenByDescending(x => x.DisContinuityCount)
                            select new
                            {
                                adviceId = so.Id,
                                shuttleOperationId = so.ShuttleOperation.Id,
                                DateTime = so.ShuttleOperation.DateTime,
                                StudentId = so.Student.Id,
                                StudentName = so.Student.Name + " " + so.Student.Surname,
                                StudentDiscontinuityCount = so.DisContinuityCount,
                                StudentMounthlyDiscontinuityCount = so.MounthlyDiscontinuityCount,
                                ParentName = so.Student.ParentName,
                                Phone = so.Student.ParentPhoneNumber,
                                DestinationName = so.ShuttleOperation.LocationRegion.Name
                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetShuttleOperationWithStudentsResponse(this ShuttleOperation entity)
        {
            var result = new
            {
                shuttleOperationId = entity.Id,
                regionName = entity.LocationRegion.Name,
                Time = entity.DateTime.Hour + ":" + (entity.DateTime.Minute < 10 ? "0" + entity.DateTime.Minute : entity.DateTime.Minute.ToString()),
                DateTime = entity.DateTime,//TODO:SAAT formati uygun oldugunda kaldıralım
                                           // Status = so.Status,
                OperationStatus = entity.ShuttleOperationStatus,
                Students = entity.ShuttleStudentOperations.Select(s => new
                {
                    ShuttleStudentOperationId = s.Id,
                    StudentId = s.Student.Id,
                    Name = s.Student.Name + " " + s.Student.Surname,
                    Status = s.OperationStatus,
                    IsCompensation = s.IsCompensation,
                    LessonRelation = new
                    {
                        PlannedLessonCount = s.LessonRelation.PlannedLessonCount,
                        CompletedLessonCount = s.LessonRelation.CompletedLessonCount
                    },

                }
                                ),
                StudentService = new
                {
                    entity.StudentService.Id,
                    entity.StudentService.Plate,
                    entity.StudentService.MaxCapacity,
                }
            };
            return new BaseServiceResponse()
            {
                ResultValue = result
            };
        }



        internal static object GetShuttleOperationListWithStudentsResponse(this IEnumerable<ShuttleOperation> shuttleOperations)
        {
            var entities = (from so in shuttleOperations
                            select new
                            {
                                shuttleOperationId = so.Id,
                                regionName = so.LocationRegion.Name,
                                Time = so.DateTime.Hour + ":" + (so.DateTime.Minute < 10 ? "0" + so.DateTime.Minute : so.DateTime.Minute.ToString()),
                                DateTime = so.DateTime,//TODO:SAAT formati uygun oldugunda kaldıralım
                                // Status = so.Status,
                                OperationStatus = so.ShuttleOperationStatus,
                                Students = so.ShuttleStudentOperations.Select(s => new
                                {
                                    ShuttleStudentOperationId = s.Id,
                                    StudentId = s.Student.Id,
                                    Name = s.Student.Name + " " + s.Student.Surname,
                                    Status = s.OperationStatus,
                                    IsCompensation = s.IsCompensation,
                                    LessonRelation = new
                                    {
                                        PlannedLessonCount = s.LessonRelation.PlannedLessonCount,
                                        CompletedLessonCount = s.LessonRelation.CompletedLessonCount
                                    },

                                }
                                ),
                                StudentService = new
                                {
                                    so.StudentService.Id,
                                    so.StudentService.Plate,
                                    so.StudentService.MaxCapacity,
                                }
                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }
        internal static object GetStudentOperationListResponse(this IEnumerable<ShuttleStudentOperation> studentOperations)
        {
            var entities = (from so in studentOperations
                            select new
                            {
                                shuttleOperationId = so.ShuttleOperation.Id,
                                ShuttleStudentOperationId = so.Id,
                                StudentId = so.Student.Id,
                                Name = so.Student.Name + " " + so.Student.Surname,
                                IsCompensation = so.IsCompensation,
                                Status = so.OperationStatus,
                                LessonRelation = new
                                {
                                    PlannedLessonCount = so.LessonRelation.PlannedLessonCount,
                                    CompletedLessonCount = so.LessonRelation.CompletedLessonCount
                                },
                                Order = so.Order// TODO:
                            }).OrderBy(x => x.Order).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }
        internal static object GetShuttleOperationResponse(this ShuttleOperation entity)
        {
            var response = new
            {
                shuttleOperationId = entity.Id,
                regionName = entity.LocationRegion.Name,
                DateTime = entity.DateTime,
                Time = entity.DateTime.Hour + ":" +
                       (entity.DateTime.Minute < 10 ? "0" + entity.DateTime.Minute : entity.DateTime.Minute.ToString()),
                Status = entity.ShuttleOperationStatus
            };
            return new BaseServiceResponse()
            {
                ResultValue = response
            };
        }

        internal static object GetShuttleOperationListResponse(this IEnumerable<ShuttleOperation> shuttleOperations)
        {
            var entities = (from so in shuttleOperations
                            select new
                            {
                                shuttleOperationId = so.Id,
                                regionName = so.LocationRegion.Name,
                                DateTime = so.DateTime,
                                Time = so.DateTime.Hour + ":" + (so.DateTime.Minute < 10 ? "0" + so.DateTime.Minute : so.DateTime.Minute.ToString()),
                                Status = so.ShuttleOperationStatus
                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetInstructorStudentsResponse(this IEnumerable<ShuttleStudentOperation> shuttleOperations)
        {
            shuttleOperations.GroupBy(en => en.ShuttleOperation.DateTime.Date);
            var entities = (from so in shuttleOperations

                            select new
                            {
                                StudentId = so.Student.Id,
                                StudentName = so.Student.Name + " " + so.Student.Surname,
                                Hour = so.ShuttleOperation.DateTime.Hour.ToString("00") + ":" + so.ShuttleOperation.DateTime.Minute.ToString("00"),
                                PhoneCallCount = so.StudentPhoneCalls.Count
                                //IsContentAdded  =  so.Student.Id % 2 ==0,
                                //so.ShuttleOperation.
                                // TODO:
                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetStudentOperations(this IEnumerable<ShuttleStudentOperation> shuttleStudentOperations)
        {
            var entities = (from so in shuttleStudentOperations.OrderByDescending(x => x.ShuttleOperation.DateTime)
                            select new
                            {
                                so.Id,
                                so.ShuttleOperation.DateTime,
                                Date = so.ShuttleOperation.DateTime.ToString("dd-MM-yyyy"),
                                Time = so.ShuttleOperation.DateTime.ToString("HH:mm"),
                                DateTimeFormat = so.ShuttleOperation.DateTime.ToString("dd-MM-yyyy HH:mm"),
                                so.LessonRelation.CompletedLessonCount,
                                so.LessonRelation.PlannedLessonCount,
                                so.IsCompensation,
                                so.OperationStatus

                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }

        internal static object GetShuttleOperationStudentLocations(this IEnumerable<ShuttleStudentOperation> shuttleStudentOperations)
        {
            var entities = (from so in shuttleStudentOperations.OrderByDescending(x => x.ShuttleOperation.DateTime)
                            select new
                            {
                                StudentId = so.Student.Id,
                                StudentName = so.Student.Name + " " + so.Student.Surname,
                                ShuttleOperationId = so.ShuttleOperation.Id,
                                LocationRegionName = so.ShuttleOperation.LocationRegion.Name,
                                so.Id,
                                so.OperationStatus,
                                so.IsCompensation,
                                Locations = so.StudentOperationLocations?.Count() > 0 ? so.StudentOperationLocations.Select(en => new { en.LocationX, en.LocationY }) : so.Student.Addresses?.Select(x => new { LocationX = x.Address?.Location?.Latitude.ToString(), LocationY = x.Address?.Location?.Longitude.ToString() })
                            }).ToList();
            return new BaseServiceResponse()
            {
                ResultValue = entities
            };
        }
    }

    public class SetShuttleOperationStatusRequest
    {
        public long ShuttleOperationId { get; set; }
        public ShuttleOperationStatus Status { get; set; }

    }
}
