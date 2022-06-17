using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;

namespace Fix.Mvc
{
    public interface IActionResultBuilder : IScoped
    {

        IActionResult Build(ModelStateDictionary dictionary);
        IActionResult Build(Exception exception);
        IActionResult Build(Exception exception, int statusCode);
        IActionResult Build(string error, string description, int statusCode);
    }

}

