using FluentValidation;
using SRM.Data.Models.Application;
using SRM.Data.Models.Individuals.StudentManagement;
using SRM.Services.Api.BaseModel;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.StudentManagement.Models
{
    public class CreateStudentAddressRequest
    {
        public string Title { get; set; }
        public string AddressInfo { get; set; }
        public string AddressDirections { get; set; }
        public int LocationRegion { get; set; }
        public string LocationX { get; set; }
        public string LocationY { get; set; }

    }

    public class UpdateStudentAddressRequest : CreateStudentAddressRequest
    {
        public long Id { get; set; }
    }

    public class UpdateStudentAddressRequestValidator : AbstractValidator<UpdateStudentAddressRequest>
    {
        public UpdateStudentAddressRequestValidator()
        {
            RuleFor(x => x.Title).NotNull().NotEmpty().WithMessage("Adres başlığı boş geçilemez");
            RuleFor(x => x.AddressInfo).NotNull().NotEmpty().WithMessage("Adres boş geçilemez");
            RuleFor(x => x.AddressInfo).NotNull().NotEmpty().MaximumLength(250).WithMessage("Adres 250 karakterden fazla olamaz");
            RuleFor(x => x.AddressInfo).NotNull().NotEmpty().MaximumLength(250).WithMessage("Adres 250 karakterden fazla olamaz");
            RuleFor(x => x.LocationRegion).NotNull().NotEmpty().WithMessage("Bölge bilgisi boş geçilemez");

        }
    }
    public static class StudentAddressModeller
    {
        public static StudentAddress CreateStudentAddressRequestToModel(this CreateStudentAddressRequest request)
        {
            var model = new StudentAddress();
            model.Address = new Address();
            model.Address.Title = request.Title;
            model.Address.AddressInfo = request.AddressInfo;
            model.Address.AddressDirections = request.AddressDirections;
            model.Address.Priority = 1;
            if (!string.IsNullOrEmpty(request.LocationX) && !string.IsNullOrEmpty(request.LocationY))
            {
                model.Address.Location = new Location
                {
                    Latitude = request.LocationX,
                    Longitude = request.LocationY,
                    LocationX = Convert.ToDouble(request.LocationX),
                    LocationY = Convert.ToDouble(request.LocationY)

                };

            }

            return model;
        }

        public static void UpdateStudentAddressRequestToModel(this UpdateStudentAddressRequest request, ref StudentAddress model)
        {
            model.Address.Title = request.Title;
            model.Address.AddressInfo = request.AddressInfo;
            model.Address.AddressDirections = request.AddressDirections;
            model.Address.Priority = 1;
            if (!string.IsNullOrEmpty(request.LocationX) && !string.IsNullOrEmpty(request.LocationY))
            {
                if (model.Address.Location == null)
                {
                    model.Address.Location = new Location
                    {
                        Latitude = request.LocationX,
                        Longitude = request.LocationY,
                        LocationX = Convert.ToDouble(request.LocationX),
                        LocationY = Convert.ToDouble(request.LocationY)

                    };
                }
                else
                {
                    model.Address.Location.Latitude = request.LocationX;
                    model.Address.Location.Longitude = request.LocationY;

                    model.Address.Location.LocationX = Convert.ToDouble(request.LocationX);
                    model.Address.Location.LocationY = Convert.ToDouble(request.LocationY);
                }
            }

        }

        public static BaseServiceResponse ToGetStudentAddresssResponse(IEnumerable<StudentAddress> model)
        {
            var entities = from StudentAddress in model
                           select new
                           {
                               StudentAddress.Id,
                               Address = new
                               {
                                   StudentAddress.Address.Title,
                                   StudentAddress.Address.AddressInfo,
                                   StudentAddress.Address.AddressDirections,
                                   LocationRegion = (StudentAddress.Address.LocationRegion != null ? new
                                   {
                                       StudentAddress.Address.LocationRegion.Id,
                                       StudentAddress.Address.LocationRegion.Name,
                                   } : null
                                   ),
                                   Location = (StudentAddress.Address.Location != null ?
                                   new
                                   {
                                       Latitude = StudentAddress.Address.Location.LocationX,
                                       Longitude = StudentAddress.Address.Location.LocationY
                                   }
                                   : new
                                   {
                                       Latitude = 0.0,
                                       Longitude = 0.0
                                   })
                               },
                           };
            return new BaseServiceResponse
            {
                ResultValue = entities.ToList()
            };
        }

        public static BaseServiceResponse ToGetStudentAddressResponse(StudentAddress StudentAddress)
        {
            var entity = new
            {
                StudentAddress.Id,
                Address = new
                {
                    StudentAddress.Address.Title,
                    StudentAddress.Address.AddressInfo,
                    StudentAddress.Address.AddressDirections,
                    LocationRegion = new
                    {
                        StudentAddress.Address.LocationRegion.Id,
                        StudentAddress.Address.LocationRegion.Name,
                    }
                }
            };
            return new BaseServiceResponse
            {
                ResultValue = entity
            };
        }
    }
}
