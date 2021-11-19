using Microsoft.Extensions.Logging;
using System;

namespace TestApp.Intrerface
{
    public interface IAppLoggerFactory : IDisposable
    {
        IAppLogger CreateLogger(string categoryName);
        void AddProvider(ILoggerProvider provider);
    }
}
