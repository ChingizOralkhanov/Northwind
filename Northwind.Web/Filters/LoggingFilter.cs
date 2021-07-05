using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northwind.Web.Filters
{
    public class LoggingFilter : ActionFilterAttribute
    {
        private readonly ILogger<LoggingFilter> _logger;
        public LoggingFilter(ILoggerFactory logger)
        {
            _logger = logger.CreateLogger<LoggingFilter>();
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            _logger.LogInformation("Actions Started");
        }
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            _logger.LogInformation("Actions Ended");
        }
    }
}
