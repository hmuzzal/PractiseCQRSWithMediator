using Microsoft.Extensions.Logging;
using System;

namespace TestApp.Intrerface
{
    public interface ILoggerProvider : IDisposable
    {
        ILogger CreateLogger(string categoryName);
    }
}
