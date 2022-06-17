using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{
    public class CreateStudentInstructorRelationRequest
    {
        public long Student { get; set; }

        public long Instructor { get; set; }

        public int Priority { get; set; }

        // public int Branch { get; set; }

        public DateTime StartDate { get; set; }

    }
    public class UpdateStudentInstructorRelationRequest : CreateStudentInstructorRelationRequest
    {
        public long Id { get; set; }

    }

    public static class StudentInstructorRelationModeller
    {
        public static StudentInstructorRelation CreateStudentInstructorRelationRequestToModel(this CreateStudentInstructorRelationRequest request)
        {
            var model = new StudentInstructorRelation();
            model.Priority = request.Priority;
            model.StartDate = request.StartDate;
            return model;
        }
        public static void UpdateStudentInstructorRelationRequestToModel(this UpdateStudentInstructorRelationRequest request, ref StudentInstructorRelation model)
        {
            model.Priority = request.Priority;
            model.StartDate = request.StartDate;
        }
        public static BaseServiceResponse ToGetStudentInstructorRelationsResponse(IEnumerable<StudentInstructorRelation> model)
        {
            var entities = from studentInstructorRelation in model
                           select new
                           {
                               studentInstructorRelation.Id,
                               Student = new
                               {
                                   studentInstructorRelation.Student.Id,
                                   studentInstructorRelation.Student.Name,
                                   studentInstructorRelation.Student.Surname,
                               },
                               Instructor = new
                               {
                                   studentInstructorRelation.Instructor.Id,
                                   studentInstructorRelation.Instructor.Name,
                                   studentInstructorRelation.Instructor.Surname,
                               },
                               studentInstructorRelation.Priority,
                               //    Branch = new
                               //    {
                               // 	   studentInstructorRelation.Branch.Id,
                               // 	   studentInstructorRelation.Branch.Name,
                               // 	   studentInstructorRelation.Branch.Description,
                               //    },
                               studentInstructorRelation.StartDate,
                           };
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

        public static BaseServiceResponse ToGetStudentInstructorRelationResponse(StudentInstructorRelation studentInstructorRelation)
        {
            if (studentInstructorRelation != null)
            {
                var entity = new
                {
                    studentInstructorRelation.Id,
                    Student = new
                    {
                        studentInstructorRelation.Student.Id,
                        studentInstructorRelation.Student.Name,
                        studentInstructorRelation.Student.Surname,
                    },
                    Instructor = new
                    {
                        studentInstructorRelation.Instructor.Id,
                        studentInstructorRelation.Instructor.Name,
                        studentInstructorRelation.Instructor.Surname,
                    },
                    studentInstructorRelation.Priority,
                    // Branch = new
                    // {
                    // 	studentInstructorRelation.Branch.Id,
                    // 	studentInstructorRelation.Branch.Name,
                    // 	studentInstructorRelation.Branch.Description,
                    // },
                    studentInstructorRelation.StartDate,
                };
                return new BaseServiceResponse
                {
                    ResultValue = entity
                };
            }
            else
            {
                return new BaseServiceResponse
                {
                    ResultValue = null
                };

            }
        }
    }
}
