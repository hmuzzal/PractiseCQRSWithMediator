using Microsoft.Extensions.Logging;
using TestApp.Intrerface;

namespace TestApp.Model
{
    public class FileLogger : IAppLogger
    {
        private readonly ILogger _logger;

        public FileLogger()
        {
        }

        public FileLogger(ILogger<object> logger)
        {
            _logger = logger;
        }

        public void LogMessage(string info)
        {
            _logger.LogInformation(info);
        }
    }
}
