using SRM.Data.Models.Parameters;
using System.Collections.Generic;

namespace SRM.Domain.Parameters.Services
{
    public interface IApplicationParameterService : IParameterService<ApplicationParameter>
    {
        IEnumerable<ApplicationParameter> GetListByName(string name);
        ApplicationParameter GetByName(string name);
        bool HasParameter(string name);
    }
}
