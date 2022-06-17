using Fix.Data;
using Fix.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Domain;
using SRM.Domain.Individuals.Exceptions.BaseException;
using SRM.Domain.Individuals.StudentManagement;
using SRM.Domain.Shuttles.OperationManagement.Services;
using SRM.Domain.Times.Services;
using SRM.Services.Api.BaseModel;
using SRM.Services.Api.Individuals.StudentManagement.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SRM.Services.Api.Individuals.StudentManagement.Controllers
{
    [EnableCors("AllowAll")]
    [Route("api/[controller]")]
    public class StudentController : Controller
    {
        private readonly IStudentDomain studentDomain;
        private readonly ILogManager logManager;
        private readonly IDateCombinationService dateCombinationService;
        private readonly ITransactionManager transactionManager;
        private readonly IShuttleService shuttleService;

        public StudentController(
            IStudentDomain studentDomain,
            ILogManager logManager,
            IDateCombinationService dateCombinationService,
            ITransactionManager transactionManager,
            IShuttleService shuttleService
            )
        {
            this.studentDomain = studentDomain ?? throw new ArgumentNullException(nameof(studentDomain));
            this.logManager = logManager ?? throw new ArgumentNullException(nameof(logManager));
            this.dateCombinationService = dateCombinationService;
            this.transactionManager = transactionManager;
            this.shuttleService = shuttleService;
        }

        #region Students
        // GET: api/values
        [HttpGet]
        public IActionResult Get()
        {
            var student = studentDomain.StudentService.GetStudents();
            var response = StudentModeller.ToGetStudentsResponse(student);
            return Ok(response);
        }

        // GET: api/values
        [HttpGet("Search")]
        public IActionResult Search(SearchStudentRequest test)
        {
            var student = studentDomain.StudentService.SearchStudents(test.Id, test.IdentityNumber, test.Name, test.Surname, test.ObstacleType, test.ReportStartDate, test.ReportEndDate, test.IsActive, test.LocationRegion);
            var response = StudentModeller.ToSearchStudentsResponse(student);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("GetStudentById/{Id}")]
        public IActionResult GetStudentById(long id)
        {
            var student = studentDomain.StudentService.GetStudentById(id);
            var response = StudentModeller.ToGetStudentResponse(student);
            return Ok(response);
        }

        // GET api/values/5
        [HttpGet("GetStudentByLocationId/{LocationId}")]
        public IActionResult GetStudentByLocationId(int locationId)
        {
            var students = studentDomain.StudentService.GetStudentByLocationId(locationId);
            var response = StudentModeller.ToGetStudentsResponse(students);
            return Ok(response);
        }

        [HttpGet("GetStudentForMobileById/{Id}")]
        public IActionResult GetStudentForMobileById(long id)
        {
            var student = studentDomain.StudentService.GetStudentById(id);
            var response = StudentModeller.ToGetStudentForMobileResponse(student);
            return Ok(response);
        }


        // GET api/values/5
        [HttpGet("GetStudentByIdentityNumber/{IdentityNumber}")]
        public IActionResult GetStudentByIdentityNumber(string identityNumber)
        {
            var student = studentDomain.StudentService.GetStudentByIdentityNumber(identityNumber);
            var response = StudentModeller.ToGetStudentResponse(student);
            return Ok(response);
        }

        // POST api/values
        [HttpPost("CreateStudent")]
        public IActionResult CreateStudent([FromBody]CreateStudentRequest request)
        {
            var student = request.CreateStudentRequestToModel();
            studentDomain.StudentService.CreateStudent(student, request.ObstacleTypes);
            return Ok(new BaseServiceResponse());
        }

        // PUT api/values/5
        [HttpPut("UpdateStudent/{Id}")]
        public IActionResult UpdateStudent(long id, [FromBody]UpdateStudentRequest request)
        {
            var student = studentDomain.StudentService.GetStudentById(id);
            request.UpdateRequestToModel(ref student);
            studentDomain.StudentService.UpdateStudent(student, request.ObstacleTypes);
            return Ok(new BaseServiceResponse());
        }

        [HttpGet("ReadStudentExcel")]
        [AllowAnonymous]
        public async Task<IActionResult> ReadStudentExcel()
        {
            StreamReader reader = new StreamReader("C:/Test/DATA.csv", Encoding.GetEncoding("windows-1254"));
            var fileContent = await reader.ReadToEndAsync();

            var lines = fileContent.Split('\n').Where(p => p.Length > 0);
            List<Student> studentList = new List<Student>();
            int order = 0;
            foreach (string item in lines)
            {
                order++;
                var line = item.Split(';');
                if (!decimal.TryParse(line[0], out decimal controlValue))
                {
                    continue;
                }
                //Kimlik numarası boş ise geç
                if (line[0].IsNullOrEmpty())
                {
                    continue;
                }
                var student = new Student();
                if (student.LocationRegion == null)
                {
                    student.LocationRegion = new LocationRegion();
                }
                student.Addresses = new List<StudentAddress>();
                for (int i = 0; i < line.Length; i++)
                {
                    var cellValue = line[i].Trim();
                    try
                    {
                        switch (i)
                        {
                            case 0:
                                student.IdentityNumber = cellValue;
                                break;
                            case 1:
                                student.Name = cellValue;
                                break;
                            case 2:
                                student.Surname = cellValue;
                                break;
                            case 3:
                                student.ParentName = cellValue;
                                break;
                            case 4:
                                student.ParentPhoneNumber = cellValue;
                                break;
                            case 5:
                                //     student.LocationRegion.Name = cellValue;
                                //     break;
                                // case 6:
                                student.Addresses.Add(new StudentAddress() { Address = new Address() { Title = "Ev Adresi", AddressInfo = cellValue, Priority = 1 } });
                                break;
                            case 6:
                                if (!cellValue.IsNullOrEmpty())
                                    student.DateOfBirth = Convert.ToDateTime(cellValue);
                                break;
                            case 7:
                                student.LocationRegion.Code = cellValue.ToInteger();
                                student.LocationRegion.Name = "Bölge " + cellValue.ToInteger();
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logManager.Logger.Error(ex, "Error on read csv. row id" + order);
                    }
                }
                studentList.Add(student);
            }
            studentDomain.StudentService.CreateExcelStudents(studentList);
            return Ok();
        }



        [HttpGet("ReadStudentAvailableExcel")]
        [AllowAnonymous]
        public async Task<IActionResult> ReadStudentAvailableExcel()
        {
            StreamReader reader = new StreamReader("C:/Test/DATA.csv", Encoding.GetEncoding("windows-1254"));
            var fileContent = await reader.ReadToEndAsync();

            var lines = fileContent.Split('\n').Where(p => p.Length > 0);
            List<StudentAvailableTime> studentAvailableTimeList = new List<StudentAvailableTime>();
            int order = 0;
            foreach (string item in lines)
            {
                order++;
                var line = item.TrimEnd('\r').Split(';');
                if (!decimal.TryParse(line[0], out decimal controlValue))
                {
                    continue;
                }
                //Kimlik numarası boş ise geç
                if (line[0].IsNullOrEmpty())
                {
                    continue;
                }

                //if (student.LocationRegion == null)
                //{
                //    student.LocationRegion = new LocationRegion();
                //}
                Student student = null;
                try
                {
                    student = studentDomain.StudentService.GetStudentByIdentityNumber(line[0]);
                }
                catch (RecordNotFoundException ex)
                {
                    Console.WriteLine(item + "- Error:" + ex.StackTrace);
                    continue;
                }
                var studentAvailableTime = new StudentAvailableTime();
                studentAvailableTime.IsAvaible = false;
                for (int i = 0; i < line.Length; i++)
                {
                    var cellValue = line[i].Trim();
                    try
                    {
                        switch (i)
                        {
                            case 0:
                                //student.IdentityNumber = cellValue;
                                break;
                            case 1:
                                //student.Name = cellValue;
                                break;
                            case 2:
                                //student.Surname = cellValue;
                                break;
                            case 3:
                                if (!cellValue.IsNullOrEmpty())
                                {
                                    if (DateTime.TryParse(cellValue, out DateTime dateTime))
                                    {
                                        studentAvailableTime.StartDate = dateTime;
                                    }
                                    else
                                    {
                                        if (!studentAvailableTime.IsIntegrated)
                                        {
                                            throw new Exception("Kısıt için başlangıç tarihi formatı hatalı. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("Kısıt için başlangıç tarihi boş olamaz. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                }
                                break;
                            case 4:

                                if (!cellValue.IsNullOrEmpty())
                                {
                                    if (DateTime.TryParse(cellValue, out DateTime dateTime))
                                    {
                                        studentAvailableTime.EndDate = dateTime;
                                    }
                                    else
                                    {
                                        if (!studentAvailableTime.IsIntegrated)
                                        {
                                            throw new Exception("Kısıt için bitiş tarihi formatı hatalı. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                        }
                                    }
                                }
                                else
                                {
                                    throw new Exception("Kısıt için bitiş tarihi boş olamaz. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                }
                                //student.ParentPhoneNumber = cellValue;
                                break;
                            case 5:
                                //     student.LocationRegion.Name = cellValue;
                                //     break;
                                // case 6:
                                if (!cellValue.IsNullOrEmpty())
                                {
                                    if (cellValue == "S" || cellValue == "P")
                                    {
                                        studentAvailableTime.IsIntegrated = cellValue == "S";
                                    }
                                    else
                                    {
                                        throw new Exception("Kısıt türü P veya S olarak girilebilir. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                    }
                                }
                                else
                                {
                                    studentAvailableTime.IsIntegrated = true;

                                }
                                //student.Addresses.Add(new StudentAddress() { Address = new Address() { Title = "Ev Adresi", AddressInfo = cellValue, Priority = 1 } });
                                break;
                            case 6:
                                if (!cellValue.IsNullOrEmpty())
                                {
                                    studentAvailableTime.IncludedDate = new Data.Models.Times.DateCombination();
                                    var days = cellValue.Split(',');
                                    foreach (var day in days)
                                    {
                                        switch (day)
                                        {
                                            case "1":
                                                studentAvailableTime.IncludedDate.Monday = true;
                                                break;
                                            case "2":
                                                studentAvailableTime.IncludedDate.Tuesday = true;
                                                break;
                                            case "3":
                                                studentAvailableTime.IncludedDate.Wednesday = true;
                                                break;
                                            case "4":
                                                studentAvailableTime.IncludedDate.Thursday = true;
                                                break;
                                            case "5":
                                                studentAvailableTime.IncludedDate.Friday = true;
                                                break;
                                            case "6":
                                                studentAvailableTime.IncludedDate.Saturday = true;
                                                break;
                                            default:
                                                throw new Exception("Parçalı kısıt için parçalı kısıt günler formatı hatalı değerler , ile ayrılmalı. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                        }
                                    }


                                }
                                break;
                            case 7:

                                if (!cellValue.IsNullOrEmpty())
                                {
                                    if (DateTime.TryParse(cellValue, out DateTime dateTime))
                                    {
                                        studentAvailableTime.StartTime = dateTime;
                                    }
                                    else
                                    {
                                        if (!studentAvailableTime.IsIntegrated)
                                        {
                                            throw new Exception("Parçalı kısıt için başlangıç saati formatı hatalı. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                        }

                                    }
                                }
                                else
                                {
                                    if (!studentAvailableTime.IsIntegrated)
                                    {
                                        throw new Exception("Parçalı kısıt için başlangıç saati zorunludur. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                    }
                                    studentAvailableTime.StartTime = null;
                                }
                                break;
                            case 8:

                                if (!cellValue.IsNullOrEmpty())
                                {
                                    if (DateTime.TryParse(cellValue, out DateTime dateTime))
                                    {
                                        studentAvailableTime.EndTime = dateTime;
                                    }
                                    else
                                    {
                                        throw new Exception("Parçalı kısıt için bitiş saati formatı hatalı. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                    }
                                    studentAvailableTime.EndTime = Convert.ToDateTime(cellValue);
                                }
                                else
                                {
                                    if (!studentAvailableTime.IsIntegrated)
                                    {
                                        throw new Exception("Parçalı kısıt için bitiş saati zorunludur. Değer:" + cellValue + " Kimlik :" + line[0] + " Sıra:" + order);
                                    }
                                    studentAvailableTime.EndTime = null;

                                }
                                break;
                            case 9:
                                studentAvailableTime.Description = cellValue;
                                break;
                        }
                    }
                    catch (Exception ex)
                    {
                        logManager.Logger.Error(ex, "Error on read csv. row id" + order);
                    }
                }
                studentAvailableTime.Student = student;
                if (!studentAvailableTime.IsIntegrated)
                    studentAvailableTime.IncludedDate = dateCombinationService.GetDateCombination(studentAvailableTime.IncludedDate);

                studentAvailableTimeList.Add(studentAvailableTime);
            }
            foreach (var item in studentAvailableTimeList)
            {
                studentDomain.StudentAvailableTimeService.Create(item);
                transactionManager.Commit();
                shuttleService.ChangeStudentOperationByAvaibleTime(item.Id);
                transactionManager.Commit();
            }

            //studentDomain.StudentService.CreateExcelStudents(studentAvailableTimeList);
            return Ok();
        }
        #endregion
    }
}
