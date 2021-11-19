using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using TestApp.Model;

namespace TestApp.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private AppLogger _appLogger;
        //private readonly ILogger _logger;
        private readonly IServiceProvider _serviceProvider;

        //public LogController(ILogger<LogController> logger, IServiceProvider serviceProvider)
        //{
        //    _logger = logger;
        //    _serviceProvider = serviceProvider;
        //}
        public LogController(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        [HttpGet("SaveLogToDb")]
        public IActionResult SaveLogToDb()
        {
            var specificLogger = _serviceProvider.GetService(typeof(ILogger<LogController>)) as ILogger<LogController>;

            _appLogger = new AppLogger(new DbLogger(specificLogger));
            try
            {
                if (1 == 2)
                {
                    return Ok();
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _appLogger.LogException(ex);
                return BadRequest();
            }
        }

        [HttpGet("WriteLogToTextFile")]
        public IActionResult WriteLogToTextFile()
        {
            var specificLogger = _serviceProvider.GetService(typeof(ILogger<LogController>)) as ILogger<LogController>;

            _appLogger = new AppLogger(new FileLogger(specificLogger));
            try
            {
                if (1 == 2)
                {
                    return Ok();
                }
                else
                {
                    throw new Exception();
                }

            }
            catch (Exception ex)
            {
                _appLogger.LogException(ex);
                return BadRequest();
            }
        }
    }
}