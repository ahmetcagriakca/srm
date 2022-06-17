using FluentValidation;
using SRM.Data.Models.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Parameters.Models
{

    public class SearchApplicationParameterRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool? IsActive { get; set; }
    }

    public class SearchApplicationParameterResponse
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool? IsActive { get; set; }
    }

    public class GetApplicationParameterResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }

    //TODO:validator
    //[Validator(typeof(CreateApplicationParameterRequestValidator))]
    public class CreateApplicationParameterRequest
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }

    public class CreateApplicationParameterRequestValidator : AbstractValidator<CreateApplicationParameterRequest>
    {
        public CreateApplicationParameterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Parametre Adı Boş Geçilemez")
                .MaximumLength(50).WithMessage("Parametre Adı Uzunluğu en fazla 50 karakter olmadır");
            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("Parametre açıklama alanı Boş Geçilemez")
                .MaximumLength(250).WithMessage("Parametre açıklama alanı Uzunluğu en fazla 250 karakter olmadır");
            RuleFor(x => x.Value)
                .NotNull().NotEmpty().WithMessage("Parametre değeri Boş Geçilemez")
                .MaximumLength(250).WithMessage("Parametre değeri Uzunluğu en fazla 250 karakter olmadır");
        }
    }

    //TODO:validator
    //[Validator(typeof(UpdateApplicationParameterRequestValidator))]
    public class UpdateApplicationParameterRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public bool IsActive { get; set; }
    }

    public class UpdateApplicationParameterRequestValidator : AbstractValidator<UpdateApplicationParameterRequest>
    {
        public UpdateApplicationParameterRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotNull().NotEmpty().WithMessage("Parametre Adı Boş Geçilemez")
                .MaximumLength(50).WithMessage("Parametre Adı Uzunluğu en fazla 50 karakter olmadır");
            RuleFor(x => x.Description)
                .NotNull().NotEmpty().WithMessage("Parametre açıklama alanı Boş Geçilemez")
                .MaximumLength(250).WithMessage("Parametre açıklama alanı Uzunluğu en fazla 250 karakter olmadır");
            RuleFor(x => x.Value)
                .NotNull().NotEmpty().WithMessage("Parametre değeri Boş Geçilemez")
                .MaximumLength(250).WithMessage("Parametre değeri Uzunluğu en fazla 250 karakter olmadır");
        }
    }

    public static class ApplicationParameterModeller
    {
        public static ApplicationParameter ToModel(this CreateApplicationParameterRequest request)
        {
            var entity = new ApplicationParameter
            {
                Name = request.Name,
                Description = request.Description,
                Value = request.Value,
                IsActive = request.IsActive,
            };
            return entity;
        }

        public static ApplicationParameter ToModel(this UpdateApplicationParameterRequest request, ApplicationParameter entity)
        {
            entity.Id = request.Id;
            entity.Name = request.Name;
            entity.Description = request.Description;
            entity.Value = request.Value;
            entity.IsActive = request.IsActive;
            return entity;
        }

        public static object ToResponse(IEnumerable<ApplicationParameter> entities)
        {
            var values = (from entity in entities
                          select new GetApplicationParameterResponse
                          {
                              Id = entity.Id,
                              Name = entity.Name,
                              Description = entity.Description,
                              Value = entity.Value,
                              IsActive = entity.IsActive
                          }).ToList();
            return new BaseServiceResponse
            {
                ResultValue = values
            };
        }

        public static object ToResponse(ApplicationParameter entity)
        {
            var value = new GetApplicationParameterResponse
            {
                Id = entity.Id,
                Name = entity.Name,
                Description = entity.Description,
                Value = entity.Value,
                IsActive = entity.IsActive
            };
            return new BaseServiceResponse
            {
                ResultValue = value
            };
        }
    }
}
