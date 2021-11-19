using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace TestApp.Controllers
{
    [ApiController]
    public class LogController : ControllerBase
    {
        private readonly ILogger _logger;

        public LogController(ILogger<LogController> logger)
        {
            _logger = logger;
        }

        [HttpGet("WriteLog")]
        public IActionResult WriteLog()
        {
            try
            {
                _logger.LogInformation("Test log message for practicing Dependency Inversion Principal");
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }
        }
    }
}