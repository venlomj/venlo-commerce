using Application.Abstractions.Messaging;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IBaseCommand
{
    private readonly ILogger<ValidationBehavior<TRequest, TResponse>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    private readonly IHttpContextAccessor _contextAccessor;

    public ValidationBehavior(IHttpContextAccessor contextAccessor, IEnumerable<IValidator<TRequest>> validators, ILogger<ValidationBehavior<TRequest, TResponse>> logger)
    {
        _contextAccessor = contextAccessor;
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        _logger.LogInformation($"{typeof(TRequest).Name} command triggered, request data: {JsonConvert.SerializeObject(request)}");

        var httpContext = _contextAccessor.HttpContext;

        // Pre-execution: validate the request
        if (_validators != null)
        {
            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
                .Select(async x => await x.ValidateAsync(context, cancellationToken))
                .SelectMany(result => result.Result.Errors)
                .Where(x => x != null)
                .ToList();

            if (failures.Any())
            {
                // Create a structured object for the validation errors
                var errorDetails = new
                {
                    Errors = failures.Select(failure => new
                    {
                        Field = failure.PropertyName,
                        Message = failure.ErrorMessage,
                        Severity = failure.Severity.ToString()
                    }),
                    Request = request
                };

                // Log the validation errors in JSON format
                _logger.LogError($"Validation failed: {JsonConvert.SerializeObject(errorDetails)}");

                // Throw validation exception
                throw new ValidationException(failures);
            }
        }

        // Continue pipeline execution if no validation errors
        return await next();
    }
}
