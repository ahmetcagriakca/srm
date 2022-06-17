using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{
    public class CreateStudentReportRequest
    {
        public string ReportNumber { get; set; }

        public int GivenHospital { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public string Description { get; set; }

        public bool IsActive { get; set; }

        public string Content { get; set; }

        //public ICollection<StudentReportDocument> Documents { get; set; }

    }
    public class UpdateStudentReportRequest : CreateStudentReportRequest
    {
        public long Id { get; set; }

    }

    public static class StudentReportModeller
    {
        public static StudentReport CreateStudentReportRequestToModel(this CreateStudentReportRequest request)
        {
            var model = new StudentReport();
            model.ReportNumber = request.ReportNumber;
            model.StartDate = request.StartDate;
            model.EndDate = request.EndDate;
            model.Description = request.Description;
            model.IsActive = request.IsActive;
            model.Content = request.Content;
            return model;
        }
        public static void UpdateStudentReportRequestToModel(this UpdateStudentReportRequest request, ref StudentReport model)
        {
            model.ReportNumber = request.ReportNumber;
            model.StartDate = request.StartDate;
            model.EndDate = request.EndDate;
            model.Description = request.Description;
            model.IsActive = request.IsActive;
            model.Content = request.Content;
        }
        public static BaseServiceResponse ToGetStudentReportsResponse(IEnumerable<StudentReport> model)
        {
            var entities = from studentReport in model
                           select new
                           {
                               studentReport.Id,
                               studentReport.ReportNumber,
                               GivenHospital = studentReport.GivenHospital,
                               studentReport.StartDate,
                               studentReport.EndDate,
                               studentReport.Content,
                           };
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

        public static BaseServiceResponse ToGetStudentReportResponse(StudentReport studentReport)
        {
            var entity = new
            {
                studentReport.Id,
                studentReport.ReportNumber,
                studentReport.GivenHospital,
                studentReport.StartDate,
                studentReport.EndDate,
                studentReport.Content,
            };
            return new BaseServiceResponse
            {
                ResultValue = entity
            };
        }
    }
}
