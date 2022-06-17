using Fix.Logging.Iteration;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Fix.Logging.Policies
{
    public abstract class BasePolicy
    {
        List<Type> types = new List<Type>();
        public ILogIterator Iterator { get; set; }
        public abstract LogLevel Level { get; }

        public BasePolicy()
        {
            Iterator = new NullIterator();
        }


        public void AddLoggerType(Type type)
        {
            types.Add(type);
        }


        public IEnumerable<Type> GetTypes()
        {
            return types.Select(x => x);
        }
    }

    public class InfoPolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Info;
    }
    public class ErrorPolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Error;
    }
    public class DebugPolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Debug;
    }
    public class WarnPolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Warn;
    }
    public class TracePolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Trace;
    }
    public class FatalPolicy : BasePolicy
    {
        public override LogLevel Level => LogLevel.Fatal;
    }
}
