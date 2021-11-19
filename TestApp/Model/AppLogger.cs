using System;
using TestApp.Intrerface;

namespace TestApp.Model
{
    public class AppLogger
    {
        private readonly IAppLogger _logger;
        public AppLogger(IAppLogger logger)
        {
            _logger = logger;
        }
        public void LogException(Exception ex)
        {
            var message = GetUserReadableMessage(ex);
            _logger.LogMessage(message);
        }

        private static string GetUserReadableMessage(Exception ex)
        {
            return ex.Message.ToString();
        }
    }
}
