using SRM.Data.Models.Individuals.InstructorManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace SRM.Services.Api.Individuals.InstructorManagement.Models
{
    public class SearchInstructorRequest
    {
        public long? Id { get; set; }

        [MaxLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }
        [MaxLength(50)]
        public int? Branch { get; set; }

        public bool? IsActive { get; set; }

    }

    public class SearchInstructorResponse
    {
        public long? Id { get; set; }


        [Required(AllowEmptyStrings = false)]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Name
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string Surname
        {
            get;
            set;
        }
        [MaxLength(50)]
        public string ObstacleType { get; set; }
        public DateTime? ReportStartDate { get; set; }
        public DateTime? ReportEndDate { get; set; }
        [Required]
        public bool IsActive { get; set; }

    }

    public class CreateInstructorRequest
    {
        [Required(AllowEmptyStrings = false)]
        [MaxLength(11)]
        [MinLength(11)]
        public string IdentityNumber { get; set; }

        [MaxLength(50)]
        public string Name { get; set; }

        [MaxLength(50)]
        public string Surname { get; set; }

        [MaxLength(250)]
        public string Email { get; set; }

        [MaxLength(250)]
        public string Phone { get; set; }

        // public List<int> Branches { get; set; }

        // [MaxLength(250)]
        // public string Address { get; set; }

        public DateTime HireDate { get; set; }

        [Required]
        public bool IsActive { get; set; }

    }
    public class UpdateInstructorRequest : CreateInstructorRequest
    {
        public long Id { get; set; }

    }

    public static class InstructorModeller
    {
        public static object ToSearchInstructorsResponse(IEnumerable<Instructor> instructors)
        {
            var entities = from s in instructors
                           select new
                           {
                               s.Id,
                               s.IdentityNumber,
                               s.Name,
                               s.Surname,
                               Branches = s.Branches?.Count > 0 ?
                                   (s.Branches?.Select(en => new { en.Branch.Id, en.Branch.Name, en.Branch.Description }))
                                   : null
                               ,
                               User = new
                               {
                                   Username = string.Empty,// s.User.Username,
                                   Email = string.Empty,//s.User.Email,
                                   MobilePhone = string.Empty,//s.User.MobilePhone,
                                   Name = string.Empty,//s.User.Name,
                                   Surname = string.Empty,//s.User.Surname,
                               },
                               isActive = s.IsActive
                           };
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

        public static Instructor CreateRequestToModel(this CreateInstructorRequest request)
        {
            var model = new Instructor
            {
                IdentityNumber = request.IdentityNumber,
                Name = request.Name,
                Surname = request.Surname,
                HireDate = request.HireDate,
                Email = request.Email,
                Phone = request.Phone,
                // model.Addresses.Add(new InstructorAddress() { Address = new Address() { Title = "Ev Adresi", AddressInfo = request.Address, Priority = 1 } });
                IsActive = request.IsActive
            };
            return model;
        }

        public static Instructor UpdateRequestToModel(this UpdateInstructorRequest request, ref Instructor model)
        {
            model.IdentityNumber = request.IdentityNumber;
            model.Name = request.Name;
            model.Surname = request.Surname;
            model.HireDate = request.HireDate;
            model.Email = request.Email;
            model.Phone = request.Phone;
            // var address = model.Addresses.FirstOrDefault();
            // address.Address.AddressInfo = request.Address;
            model.IsActive = request.IsActive;
            return model;
        }


        public static object ToGetInstructorsResponse(IEnumerable<Instructor> instructors)
        {
            return new BaseServiceResponse
            {
                ResultValue = instructors
            };

        }

        public static object ToGetInstructorResponse(Instructor instructor)
        {
            return new BaseServiceResponse
            {
                ResultValue = new
                {
                    instructor.Id,
                    instructor.IdentityNumber,
                    instructor.Name,
                    instructor.Surname,
                    instructor.Phone,
                    instructor.Email,
                    instructor.HireDate,
                    Branches = instructor.Branches?.Select(en => new { en.Branch.Id, en.Branch.Name, en.Branch.Description }),
                    Address = instructor.Addresses.FirstOrDefault()?.Address.AddressInfo,
                    instructor.IsActive
                }
            };

        }

    }
}
