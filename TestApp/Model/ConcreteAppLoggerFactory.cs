using TestApp.Intrerface;

namespace TestApp.Model
{
    class ConcreteAppLoggerFactory : AppLoggerFactory
    {
        private readonly IAppLogger _appLogger;

        public ConcreteAppLoggerFactory(IAppLogger appLogger)
        {
            _appLogger = appLogger;
        }
        public override AppLogger CreateAppLogger()
        {
            return new AppLogger(_appLogger);
        }
    }
}
