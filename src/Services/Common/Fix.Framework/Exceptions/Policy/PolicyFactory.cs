using Fix.Exceptions.Configuration;
using Fix.Exceptions.Iteration;
using System;
using System.Linq;

namespace Fix.Exceptions.Policy
{
    public class PolicyFactory : IPolicyFactory
    {
        private readonly ExceptionConfig exceptionConfig;
        private readonly IIteratorFactory iteratorFactory;

        public PolicyFactory(ExceptionConfig exceptionConfig, IIteratorFactory iteratorFactory)
        {
            this.exceptionConfig = exceptionConfig ?? throw new ArgumentNullException(nameof(exceptionConfig));
            this.iteratorFactory = iteratorFactory ?? throw new ArgumentNullException(nameof(iteratorFactory));
        }

        public bool TryGetConfig(Type exceptionType, out PolicyConfig config)
        {
            config = exceptionConfig.Policies.Where(p => p.ExceptionType == exceptionType.FullName).FirstOrDefault();
            if (config == null)
            {
                if (exceptionType.BaseType != null)
                {
                    config = exceptionConfig.Policies.Where(p => p.ExceptionType == exceptionType.BaseType.FullName).FirstOrDefault();
                }
                if (config == null)
                {
                    config = exceptionConfig.Policies.Where(p => p.ExceptionType == "*").FirstOrDefault();
                }
            }

            return (config != null);

        }
        public Policy Create(Type exceptionType)
        {
            if (TryGetConfig(exceptionType, out PolicyConfig config))
            {
                var typeNames = config.HandlerAlias.SelectMany(x => exceptionConfig.HandlerTypes.Where(k => k.Key == x).Select(d => d.Value));

                var description = new Policy();
                description.Types = typeNames.Select(x => Type.GetType(x, true, false)).ToList();
                description.Iterator = iteratorFactory.Create(config.IteratorType);
                return description;
            }
            else
            {
                throw new Exception($"No matching policy for {exceptionType}");
            }
        }
    }
}
