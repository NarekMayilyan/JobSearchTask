using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Net;
using System.Threading.Tasks;

namespace JobSearch.WEB.Filters
{
    public class ExceptionHandlerFilter : IExceptionFilter
    {
        private readonly IWebHostEnvironment _env;
        private readonly ILogger _logger;

        public ExceptionHandlerFilter(IWebHostEnvironment env, ILogger<ExceptionHandlerFilter> logger)
        {
            _env = env;
            _logger = logger;
        }

        public void OnException(ExceptionContext context)
        {
            if (context.Exception is ArgumentException)
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status400BadRequest,
                    Detail = "Please refer to the errors property for additional details.",
                    Title = "Bad Request",
                };

                problemDetails.Errors.Add("DomainValidations", new[] { context.Exception.Message });

                context.Result = new BadRequestObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            else if (context.Exception is EntryPointNotFoundException)
            {
                var problemDetails = new ValidationProblemDetails()
                {
                    Instance = context.HttpContext.Request.Path,
                    Status = StatusCodes.Status404NotFound,
                    Detail = "Please refer to the errors property for additional details.",
                    Title = "Entry not found"
                };

                problemDetails.Errors.Add("Conflict", new[] { context.Exception.Message });

                context.Result = new NotFoundObjectResult(problemDetails);
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
            }
            else if (context.Exception is TaskCanceledException)
            {
                _logger.LogError(context.Exception, context.Exception.Message);
            }
            else
            {
                _logger.LogError(context.Exception, context.Exception.Message);

                var json = new JsonErrorResponse
                {
                    Messages = new[] { "An error occur.Try it again." }
                };

                if (_env.IsDevelopment())
                {
                    json.DeveloperMessage = context.Exception;
                }

                var objectResult = new ObjectResult(json) { StatusCode = StatusCodes.Status500InternalServerError };

                context.Result = objectResult;
                context.HttpContext.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }
        }

        private class JsonErrorResponse
        {
            public string[] Messages { get; set; }

            public object DeveloperMessage { get; set; }
        }
    }
}
