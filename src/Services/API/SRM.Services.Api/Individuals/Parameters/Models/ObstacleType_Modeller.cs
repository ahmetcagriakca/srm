using FluentValidation;
using SRM.Data.Models.Individuals.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.Parameters.Models
{
    public class SearchObstacleTypeRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SearchObstacleTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetObstacleTypeResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateObstacleTypeRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateObstacleTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateObstacleTypeRequestValidator : AbstractValidator<CreateObstacleTypeRequest>
    {
        public CreateObstacleTypeRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olmalıdır");
        }
    }

    public static class ObstacleTypeModeller
    {
        public static ObstacleType ToModel(this CreateObstacleTypeRequest request)
        {
            var entity = new ObstacleType
            {
                Description = request.Description,
                Name = request.Name,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static ObstacleType ToModel(this UpdateObstacleTypeRequest request, ObstacleType entity)
        {
            entity.Id = request.Id;
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.IsActive = request.IsActive;
            return entity;
        }

        public static object ToResponse(IEnumerable<ObstacleType> entities)
        {
            var values = from entity in entities
                         select new GetObstacleTypeResponse
                         {
                             Id = entity.Id,
                             Description = entity.Description,
                             Name = entity.Name,
                             IsActive = entity.IsActive,

                         };
            return new BaseServiceResponse
            {
                ResultValue = values.ToList()
            };
        }

        public static object ToResponse(ObstacleType entity)
        {
            var value = new GetObstacleTypeResponse
            {
                Id = entity.Id,
                Description = entity.Description,
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
