using System.Diagnostics;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Api.Middlewares
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<GlobalExceptionHandlerMiddleware> _logger;
        private readonly IHostEnvironment _environment;

        public GlobalExceptionHandlerMiddleware(RequestDelegate next, ILogger<GlobalExceptionHandlerMiddleware> logger, IHostEnvironment environment)
        {
            _next = next;
            _logger = logger;
            _environment = environment;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext); // Proceed with the next middleware
            }
            catch (ValidationException ex)
            {
                var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
                _logger.LogError(ex, "Validation failed with trace id {TraceId}", traceId);

                var problemDetails = new ProblemDetails
                {
                    Status = StatusCodes.Status400BadRequest,
                    Title = "Validation Error",
                    Extensions = { ["traceId"] = traceId },
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}",
                    Detail = "One or more validation errors occurred."
                };

                // Ensure no duplicate error messages for each property
                var groupedErrors = ex.Errors
                    .GroupBy(e => e.PropertyName)
                    .ToDictionary(
                        group => group.Key,
                        group => group.Select(e => e.ErrorMessage).Distinct().ToArray()); // Use Distinct to avoid duplicates

                problemDetails.Extensions["errors"] = groupedErrors;

                httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
            catch (Exception ex)
            {
                var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
                _logger.LogError(ex, "Could not process a request on machine {MachineName} with trace id {TraceId}",
                    Environment.MachineName, traceId);

                (int statusCode, string title) = MapException(ex);

                var problemDetails = new ProblemDetails
                {
                    Status = statusCode,
                    Title = title,
                    Extensions = { ["traceId"] = traceId },
                    Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
                };

                if (!_environment.IsProduction())
                {
                    problemDetails.Detail = ex.Message;
                }

                httpContext.Response.StatusCode = statusCode;
                httpContext.Response.ContentType = "application/json";
                await httpContext.Response.WriteAsJsonAsync(problemDetails);
            }
        }

        private (int statusCode, string title) MapException(Exception exception)
        {
            // Map your exceptions to the appropriate status code and title
            if (exception is NotImplementedException)
            {
                return (StatusCodes.Status501NotImplemented, "Not Implemented");
            }
            if (exception is UnauthorizedAccessException)
            {
                return (StatusCodes.Status401Unauthorized, "Unauthorized");
            }

            // Fallback for general exceptions
            return (StatusCodes.Status500InternalServerError, "Internal Server Error");
        }
    }

}
