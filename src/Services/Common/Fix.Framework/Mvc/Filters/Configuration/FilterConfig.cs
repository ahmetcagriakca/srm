using Fix.Configuration;

namespace Fix.Mvc.Filters.Configuration
{
    public class FilterConfig : IConfigurationBase
    {
        public bool IsValid()
        {
            return true;
        }

        public bool IsValid(out string message)
        {
            message = string.Empty;
            return IsValid();
        }
    }
}
