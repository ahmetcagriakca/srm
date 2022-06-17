using FluentValidation;
using SRM.Data.Models.Shuttles.Parameters;
using SRM.Services.Api.BaseModel;
using System.Collections.Generic;
using System.Linq;

namespace SRM.Services.Api.Shuttles.Parameters.Models
{

    public class SearchLocationRegionRequest
    {
        public int? Id { get; set; }
        public string Name { get; set; }
        public int? Code { get; set; }
        public bool? IsActive { get; set; }
        public int? SubRegion { get; set; }
    }

    public class SearchLocationRegionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
    }

    public class GetLocationRegionResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<LocationRegion> SubRegions { get; set; }
    }

    //TODO:validator
    //[Validator(typeof(CreateLocationRegionRequestValidator))]
    public class CreateLocationRegionRequest
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<int> SubRegionIds { get; set; }
    }

    public class CreateLocationRegionRequestValidator : AbstractValidator<CreateLocationRegionRequest>
    {
        public CreateLocationRegionRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(256).WithMessage("Bölge Adı Boş Geçilemez");
            RuleFor(x => x.Name).MaximumLength(256).WithMessage("Bölge Adı Uzunluğu en fazla 256 karakter olmadır");
            RuleFor(x => x.Code).NotNull().NotEmpty().WithMessage("Bölge Kodu boş geçilemez");
        }
    }

    //TODO:validator
    //[Validator(typeof(UpdateLocationRegionRequestValidator))]
    public class UpdateLocationRegionRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Code { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<int> SubRegionIds { get; set; }
    }

    public class UpdateLocationRegionRequestValidator : AbstractValidator<UpdateLocationRegionRequest>
    {
        public UpdateLocationRegionRequestValidator()
        {
            RuleFor(x => x.Id).NotNull().NotEmpty().WithMessage("Id alanı boş geçilemez");
            RuleFor(x => x.Name).NotNull().NotEmpty().MaximumLength(256).WithMessage("Bölge Adı Boş Geçilemez");
            RuleFor(x => x.Name).MaximumLength(256).WithMessage("Bölge Adı Uzunluğu en fazla 256 karakter olmadır");
            RuleFor(x => x.Code).NotNull().NotEmpty().NotEqual(0).WithMessage("Bölge Kodu boş geçilemez");
        }
    }

    public static class LocationRegionModeller
    {
        public static LocationRegion ToModel(this CreateLocationRegionRequest request)
        {
            var entity = new LocationRegion
            {
                IsActive = request.IsActive,
                Name = request.Name,
                Code = request.Code

            };
            return entity;
        }

        public static LocationRegion ToModel(this UpdateLocationRegionRequest request, LocationRegion entity)
        {
            entity.Id = request.Id;
            entity.IsActive = request.IsActive;
            entity.Name = request.Name;
            entity.Code = request.Code;
            return entity;
        }

        public static object ToResponse(IEnumerable<LocationRegion> entities)
        {
            var values = (from entity in entities
                          select new GetLocationRegionResponse
                          {
                              Id = entity.Id,
                              IsActive = entity.IsActive,
                              Name = entity.Name,
                              Code = entity.Code,
                              SubRegions = entity
                              .RegionRelations?
                              .Select(en => en.SubRegion)
                              .Select(en =>
                                 new LocationRegion() { Id = en.Id, Name = en.Name }
                              )
                          }).ToList();
            return new BaseServiceResponse
            {
                ResultValue = values
            };
        }

        public static object ToResponse(LocationRegion entity)
        {
            var value = new GetLocationRegionResponse
            {
                Id = entity.Id,
                IsActive = entity.IsActive,
                Name = entity.Name,
                Code = entity.Code,
                SubRegions = entity
                .RegionRelations
                .Select(en => en.SubRegion)
                .Select(en =>
                    new LocationRegion() { Id = en.Id, Name = en.Name }
                )
            };
            return new BaseServiceResponse
            {
                ResultValue = value
            };
        }
    }
}
