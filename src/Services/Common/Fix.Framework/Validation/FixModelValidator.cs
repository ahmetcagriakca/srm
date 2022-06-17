using FluentValidation;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fix.Validation
{
    public class FixModelValidatorProvider : IModelValidatorProvider
    {
        public void CreateValidators(ModelValidatorProviderContext context)
        {

            if (context.ModelMetadata.MetadataKind == ModelMetadataKind.Parameter)
            {
                context.Results.Add(new ValidatorItem
                {
                    IsReusable = false,
                    Validator = new FixModelValidator()
                });
            }
        }
        public class FixModelValidator : IModelValidator
        {
            public IEnumerable<ModelValidationResult> Validate(ModelValidationContext context)
            {
                if (context == null)
                    throw new ArgumentNullException(nameof(context));

                var modelType = context.ModelMetadata.ModelType;

                var factory = context.ActionContext.HttpContext.RequestServices.GetService(typeof(IValidatorFactory)) as IValidatorFactory;

                var validator = factory.GetValidator(modelType);
                if (validator != null)
                {
                    var result = validator.Validate(context.Model);
                    if (!result.IsValid)
                        return result.Errors.Select(x => new ModelValidationResult(x.PropertyName, x.ErrorMessage));
                }
                return Enumerable.Empty<ModelValidationResult>();
            }
        }
    }
}
