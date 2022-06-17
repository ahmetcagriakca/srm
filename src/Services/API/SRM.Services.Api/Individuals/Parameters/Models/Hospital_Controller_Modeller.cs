using FluentValidation;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.Parameters.Models
{
    public class SearchHospitalRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SearchHospitalResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetHospitalResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateHospitalRequest
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateHospitalRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateHospitalRequestValidator : AbstractValidator<CreateHospitalRequest>
    {
        public CreateHospitalRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
        }
    }

    public static class HospitalModeller
    {
        public static Hospital ToModel(this CreateHospitalRequest request)
        {
            var entity = new Hospital
            {
                Name = request.Name,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static Hospital ToModel(this UpdateHospitalRequest request, Hospital entity)
        {
            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.IsActive = request.IsActive;
            return entity;
        }

        public static object ToResponse(IEnumerable<Hospital> entities)
        {
            var values = from entity in entities
                         select new GetHospitalResponse
                         {
                             Id = entity.Id,
                             Name = entity.Name,
                             IsActive = entity.IsActive,
                         };
            return new BaseServiceResponse
            {
                ResultValue = values.ToList()
            };
        }

        public static object ToResponse(Hospital entity)
        {
            var value = new GetHospitalResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                IsActive = entity.IsActive,
            };
            return new BaseServiceResponse
            {
                ResultValue = value
            };
        }
    }
}
