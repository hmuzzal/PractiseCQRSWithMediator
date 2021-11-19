using Microsoft.Extensions.Logging;
using TestApp.Intrerface;

namespace TestApp.Model
{
    public class DbLogger : IAppLogger
    {

        private readonly ILogger _logger;

        public DbLogger()
        {
        }

        public DbLogger(ILogger<object> logger)
        {
            _logger = logger;
        }

        public void LogMessage(string info)
        {
            _logger.LogError(info);
        }
    }
}
