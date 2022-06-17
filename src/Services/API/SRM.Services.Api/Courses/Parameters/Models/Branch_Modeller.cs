using FluentValidation;
using SRM.Data.Models.Courses.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Individuals.Parameters.Models
{
    public class SearchBranchRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool? IsActive { get; set; }
    }
    public class SearchBranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetBranchResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateBranchRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateBranchRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateBranchRequestValidator : AbstractValidator<CreateBranchRequest>
    {
        public CreateBranchRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().WithMessage("Boş geçilemez");
            RuleFor(x => x.Description).NotNull().NotEmpty().MaximumLength(250).WithMessage("Açıklama en fazla 250 karakter olmalıdır");
        }
    }

    public static class BranchModeller
    {
        public static Branch ToModel(this CreateBranchRequest request)
        {
            var entity = new Branch
            {
                Description = request.Description,
                Name = request.Name,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static Branch ToModel(this UpdateBranchRequest request, Branch entity)
        {
            entity.Id = request.Id;
            entity.Description = request.Description;
            entity.Name = request.Name;
            entity.IsActive = request.IsActive;
            return entity;
        }

        public static BaseServiceResponse ToResponse(IEnumerable<Branch> entities)
        {
            var values = from entity in entities
                         select new GetBranchResponse
                         {
                             Id = entity.Id,
                             Description = entity.Description,
                             Name = entity.Name,
                             IsActive = entity.IsActive,

                         };
            return new BaseServiceResponse
            {
                ResultValue = values
            };
        }

        public static BaseServiceResponse ToResponse(Branch entity)
        {
            var value = new GetBranchResponse
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
