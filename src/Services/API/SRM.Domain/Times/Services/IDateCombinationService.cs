using Fix;
using SRM.Data.Models.Times;

namespace SRM.Domain.Times.Services
{
    public interface IDateCombinationService : IDependency
    {
        bool HasDateCombination();
        bool CreateDateCombination();
        DateCombination GetDateCombination(DateCombination dateCombination);

    }
}
