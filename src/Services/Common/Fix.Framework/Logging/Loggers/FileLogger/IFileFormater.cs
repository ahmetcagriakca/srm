using System.Text;

namespace Fix.Logging.Loggers.FileLogger
{
    public interface IFileFormater : IScoped
    {
        StringBuilder Format<T>(LogData<T> logData) where T : class;
    }
}
