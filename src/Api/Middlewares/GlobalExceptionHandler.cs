using System.Diagnostics;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace Api.Middlewares
{
    public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IHostEnvironment environment)
        : IExceptionHandler
    {
        private const bool IsLastStopInPipeline = true;
        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
            logger.LogError(exception, "Could not process a request on machine {MachineName} with trace id {TraceId}",
                Environment.MachineName, traceId);

            (int statusCode, string title) = MapException(exception);

            var problemDetails = new ProblemDetails
            {
                Status = statusCode,
                Title = title,
                Extensions = { ["traceId"] = traceId },
                Instance = $"{httpContext.Request.Method} {httpContext.Request.Path}"
            };

            if (!environment.IsProduction())
            {
                problemDetails.Detail = exception.Message;
            }

            await httpContext.Response
                .WriteAsJsonAsync(problemDetails, cancellationToken);

            return IsLastStopInPipeline;
        }

        private (int statusCode, string title) MapException(Exception exception)
        {
            // Map your exceptions to the appropriate status code and title
            // Example: 
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
