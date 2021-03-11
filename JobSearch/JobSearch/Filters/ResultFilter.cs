using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace JobSearch.WEB.Filters
{
    public class ResultFilter : IResultFilter
    {
        public ResultFilter()
        { }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            // Method intentionally left empty.
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            if (context.Result is ObjectResult &&
               ((ObjectResult)context.Result).Value is string)
            {
                switch (((ObjectResult)context.Result).StatusCode)
                {
                    case StatusCodes.Status400BadRequest:

                        var badRequestDetails = new ValidationProblemDetails()
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status400BadRequest,
                            Detail = "Please refer to the errors property for additional details.",
                            Title = "Bad Request"
                        };

                        badRequestDetails.Errors.Add("DomainValidations", new[] { ((ObjectResult)context.Result).Value.ToString() });

                        context.Result = new BadRequestObjectResult(badRequestDetails);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        break;

                    case StatusCodes.Status404NotFound:

                        var notFoundDetails = new ValidationProblemDetails()
                        {
                            Instance = context.HttpContext.Request.Path,
                            Status = StatusCodes.Status404NotFound,
                            Detail = "Please refer to the errors property for additional details.",
                            Title = "Entry not found"
                        };

                        notFoundDetails.Errors.Add("Conflict", new[] { ((ObjectResult)context.Result).Value.ToString() });

                        context.Result = new NotFoundObjectResult(notFoundDetails);
                        context.HttpContext.Response.StatusCode = (int)HttpStatusCode.NotFound;
                        break;
                    default:
                        break;
                }
            }
        }
    }
}
