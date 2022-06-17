using Fix.Configuration;

namespace Fix.Mvc.Middleware.Config
{
    public class MiddlewareConfig : IConfigurationBase
    {
        public bool UseMiddleware { get; set; }
        public bool IsValid()
        {
            return true;
        }

        public bool IsValid(out string message)
        {
            message = "";
            return true;
        }
    }
}
