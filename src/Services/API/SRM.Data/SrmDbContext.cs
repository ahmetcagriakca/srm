using Fix.Security;
using Microsoft.EntityFrameworkCore;
using SRM.Data.Models.Application;
using SRM.Data.Models.CallManagement;
using SRM.Data.Models.CorporationManagement;
using SRM.Data.Models.Courses;
using SRM.Data.Models.Courses.Parameters;
using SRM.Data.Models.Individuals.InstructorManagement;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Parameters;
using SRM.Data.Models.Shuttles;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Data.Models.Shuttles.TemplateManagement;
using SRM.Data.Models.Times;
using System;

namespace SRM.Data
{
    public class SrmDbContext : DbContext
    {
        public Guid ID
        {
            get
            {
                return Guid.NewGuid();
            }
        }

        public SrmDbContext(DbContextOptions<SrmDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.TraceableEntity<AuthenticationEntity>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.UserId).IsRequired();
                entity.Property(x => x.Token).HasMaxLength(512).IsRequired();
                entity.Property(x => x.RefreshToken).HasMaxLength(512).IsRequired();
                entity.Property(x => x.ExpiredOn).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
            });
            #region Parameters

            modelBuilder.TraceableEntity<ApplicationParameter>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(250);
                entity.Property(x => x.Value).HasMaxLength(250).IsRequired();
            });


            modelBuilder.TraceableEntity<ObstacleType>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(250);

            });


            modelBuilder.TraceableEntity<Branch>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(250);

            });

            modelBuilder.TraceableEntity<Hospital>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.IsActive).HasMaxLength(100).IsRequired();
            });
            #endregion

            #region Application Models

            modelBuilder.TraceableEntity<Document>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).HasMaxLength(256).IsRequired();
                entity.Property(x => x.FileName).HasMaxLength(256).IsRequired();
                entity.Property(x => x.FileType).HasMaxLength(20).IsRequired();
                entity.Property(x => x.ContentPath).HasMaxLength(256).IsRequired();
            });

            modelBuilder.TraceableEntity<Address>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Title).HasMaxLength(250).IsRequired();
                entity.Property(x => x.AddressInfo).HasMaxLength(500).IsRequired();
                entity.HasOne(x => x.Neighborhood);

                entity.Property(x => x.Area);
                entity.Property(x => x.AddressDirections).HasMaxLength(500);
                entity.Property(x => x.Priority).IsRequired();
                entity.HasOne(x => x.Location);
                entity.HasOne(x => x.LocationRegion);

            });
            modelBuilder.TraceableEntity<City>(entity =>
            {
                entity.HasKey(x => x.Id);
                //TODO plaka kodu uniqe alan yapılacak
                entity.Property(x => x.PlateCode).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(100);


            });
            modelBuilder.TraceableEntity<County>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.HasOne(x => x.City);
            });
            modelBuilder.TraceableEntity<Neighborhood>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.HasOne(x => x.County);

            });
            modelBuilder.TraceableEntity<Location>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Latitude).IsRequired();
                entity.Property(x => x.Longitude).IsRequired();
                entity.Property(x => x.LocationX);
                entity.Property(x => x.LocationY);
            });
            #endregion

            #region Courses Models
            modelBuilder.TraceableEntity<Lesson>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Description).HasMaxLength(250).IsRequired();
                entity.HasOne(x => x.Branch);

            });
            modelBuilder.TraceableEntity<LessonSession>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Header).HasMaxLength(100).IsRequired();
                entity.Property(x => x.Content).HasMaxLength(1000).IsRequired();
                entity.Property(x => x.StartDate);
                entity.HasOne(x => x.Instructor).WithMany(u => u.LessonSessions);
                entity.HasOne(x => x.Lesson).WithMany(u => u.LessonSessions);
            });

            modelBuilder.TraceableEntity<LessonContentDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.LessonSession);
                entity.HasOne(x => x.Document);
                entity.Property(x => x.IsDelete).IsRequired();

            });
            #endregion

            #region Instructor Models
            modelBuilder.TraceableEntity<Instructor>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Surname).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Email).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Phone).HasMaxLength(50).IsRequired();
                entity.Property(x => x.HireDate).IsRequired();
                entity.Property(x => x.HireDate).IsRequired();
                entity.Property(x => x.IsActive);
            });

            modelBuilder.TraceableEntity<InstructorAddress>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Instructor).WithMany(x => x.Addresses);
                entity.HasOne(x => x.Address);
            });

            modelBuilder.TraceableEntity<InstructorBranch>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Instructor).WithMany(x => x.Branches);
                entity.HasOne(x => x.Branch);
            });
            #endregion

            #region Student Models
            modelBuilder.TraceableEntity<Student>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.IdentityNumber).HasMaxLength(11).IsRequired();
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.Surname).HasMaxLength(50).IsRequired();
                entity.Property(x => x.DateOfBirth).IsRequired();
                entity.Property(x => x.ParentName).HasMaxLength(100).IsRequired();
                entity.Property(x => x.ParentPhoneNumber).HasMaxLength(200);
                entity.Property(x => x.CourseStartDate);
                entity.Property(x => x.IsActive);
                entity.HasMany(x => x.ShuttleStudentTemplates);
                entity.HasMany(x => x.StudentContacts);

            });

            modelBuilder.TraceableEntity<StudentObstacleType>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student).WithMany(x => x.ObstacleTypes);
                entity.HasOne(x => x.ObstacleType);
            });

            modelBuilder.TraceableEntity<StudentAddress>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student).WithMany(x => x.Addresses);
                entity.HasOne(x => x.Address);
            });


            modelBuilder.TraceableEntity<StudentLesson>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student).WithMany(x => x.Lessons);
                entity.HasOne(x => x.Lesson);
            });


            modelBuilder.TraceableEntity<StudentLessonSession>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.StudentLesson).WithMany(x => x.LessonSessions);
                entity.HasOne(x => x.LessonSession);
            });

            modelBuilder.TraceableEntity<StudentReport>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.ReportNumber).IsRequired();
                entity.HasOne(x => x.GivenHospital);
                entity.Property(x => x.StartDate);
                entity.Property(x => x.EndDate);
                entity.Property(x => x.Description).HasMaxLength(250);
                entity.Property(x => x.IsActive).IsRequired();
                entity.Property(x => x.Content).HasMaxLength(1000).IsRequired();
                entity.HasOne(x => x.Student);
            });

            modelBuilder.TraceableEntity<StudentReportDocument>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.StudentReport);
                entity.HasOne(x => x.Document);
                entity.Property(x => x.IsDelete).IsRequired();
            });

            modelBuilder.TraceableEntity<StudentAvailableTime>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student);
                entity.Property(x => x.IsIntegrated);
                entity.Property(x => x.StartDate);
                entity.Property(x => x.EndDate);
                entity.Property(x => x.StartTime);
                entity.Property(x => x.EndTime);
                entity.HasOne(x => x.IncludedDate);
                entity.Property(x => x.Description).HasMaxLength(250);
            });

            modelBuilder.TraceableEntity<StudentContact>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student).WithMany(x => x.StudentContacts);
                entity.Property(x => x.Name);
                entity.Property(x => x.Number);
            });
            #endregion

            #region Times Models
            modelBuilder.TraceableEntity<DateCombination>(entity =>
                    {
                        entity.HasKey(x => x.Id);
                        entity.Property(x => x.Monday);
                        entity.Property(x => x.Tuesday);
                        entity.Property(x => x.Wednesday);
                        entity.Property(x => x.Thursday);
                        entity.Property(x => x.Friday);
                        entity.Property(x => x.Saturday);
                        entity.Property(x => x.Sunday);
                    });
            #endregion

            #region Shuttles Models

            modelBuilder.TraceableEntity<LocationRegion>(entity =>
             {
                 entity.HasKey(x => x.Id);
                 entity.Property(x => x.Name).HasMaxLength(100).IsRequired();
                 entity.Property(x => x.Code).HasMaxLength(5).IsRequired();
                 entity.HasMany(x => x.Students).WithOne(x => x.LocationRegion);
                 entity.Property(x => x.IsActive);
             });

            modelBuilder.TraceableEntity<LocationRegionRelation>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.MainRegion).WithMany(x => x.RegionRelations);
                entity.HasOne(x => x.SubRegion);
            });

            modelBuilder.TraceableEntity<ShuttleTemplate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.LocationRegion);
                entity.Property(x => x.DayOfWeek).IsRequired();
                entity.Property(x => x.Time).IsRequired();
                entity.HasMany(x => x.ShuttleStudentTemplates).WithOne(x => x.ShuttleTemplate);
                entity.Property(x => x.IsActive);
                entity.Property(x => x.IsDelete);
            });

            modelBuilder.TraceableEntity<ShuttleStudentTemplate>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.ShuttleTemplate).WithMany(x => x.ShuttleStudentTemplates);
                entity.HasOne(x => x.Student);
                entity.Property(x => x.Order);
                entity.Property(x => x.IsActive);
                entity.Property(x => x.LessonCount);
            });

            modelBuilder.TraceableEntity<ShuttleOperation>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.ShuttleTemplate);
                entity.Property(x => x.DateTime).IsRequired();
                entity.Property(x => x.ShuttleOperationStatus);
                entity.Property(x => x.OperationStartTime);
                entity.Property(x => x.OperationEndTime);
                entity.HasOne(x => x.LocationRegion);
            });


            modelBuilder.TraceableEntity<ShuttleStudentOperation>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student);
                entity.Property(x => x.Status);//TODO: operasyon statusları tasındıktan sonra kaldırılmalı
                entity.Property(x => x.OperationStatus);
                entity.Property(x => x.IsCompensation);
                entity.Property(x => x.Order);
                entity.HasOne(x => x.LessonRelation);
            });

            modelBuilder.TraceableEntity<ShuttleStudentOperasionLessonRelation>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.PlannedLessonCount);
                entity.Property(x => x.CompletedLessonCount);
                entity.HasOne(x => x.ShuttleStudentOperation).WithOne(x => x.LessonRelation).HasForeignKey<ShuttleStudentOperasionLessonRelation>(x => x.ShuttleStudentOperationRef);
            });


            modelBuilder.TraceableEntity<ShuttleStudentOperationAdvice>(entity =>
                       {
                           entity.HasKey(x => x.Id);
                           entity.HasOne(x => x.ShuttleOperation);
                           entity.HasOne(x => x.Student);
                           entity.HasOne(x => x.ShuttleStudentOperation);
                           entity.Property(x => x.AdviceStatus);
                           entity.Property(x => x.DisContinuityCount);
                           entity.Property(x => x.MounthlyDiscontinuityCount);
                       });

            modelBuilder.TraceableEntity<StudentService>(entity =>
                       {
                           entity.HasKey(x => x.Id);
                           entity.Property(x => x.Plate);
                           entity.Property(x => x.MaxCapacity);
                           entity.HasMany(x => x.ShuttleTemplates);
                           entity.HasMany(x => x.ShuttleOperations);
                       });

            modelBuilder.TraceableEntity<StudentOperationLocation>(entity =>
                       {
                           entity.HasKey(x => x.Id);
                           entity.HasOne(x => x.StudentOperation);
                           entity.Property(x => x.LocationX);
                           entity.Property(x => x.LocationY);
                       });


            #endregion

            #region StudentCalls
            modelBuilder.TraceableEntity<StudentPhoneCall>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.HasOne(x => x.Student);
                entity.Property(x => x.Description);
                entity.Property(x => x.CallType);
                entity.Property(x => x.StudentAnswer);
                entity.HasOne(x => x.ShuttleStudentOperation);
            });
            #endregion

            #region Corporation

            modelBuilder.TraceableEntity<Company>(entity =>
            {
                entity.HasKey(x => x.Id);
                entity.Property(x => x.Name).HasMaxLength(50).IsRequired();
                entity.Property(x => x.LongName).HasMaxLength(250).IsRequired();
                entity.Property(x => x.IsActive).IsRequired();
            });

            #endregion
        }
    }
}
