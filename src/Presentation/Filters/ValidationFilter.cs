using System.Diagnostics;
using FluentValidation;
using FluentValidation.Results;
using Nett.Core;

namespace HealthMed.Presentation.Filters;

internal sealed class ValidationFilter<T> : IEndpointFilter
{
    private const string ErrorType = "https://tools.ietf.org/html/rfc7231#section-6.5.1";
    private const string ErrorTitle = "One or more validation errors occurred.";
    private const int HttpStatusCode = 400;

    public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
    {
        if (context.Arguments.FirstOrDefault(x => x?.GetType() == typeof(T)) is T requestData)
        {
            var validator = context.HttpContext.RequestServices.GetRequiredService<IValidator<T>>();
            var validationResult = await validator.ValidateAsync(requestData);

            if (validationResult.IsValid is false)
            {
                var errorDetails = validationResult.Errors.Select(CreateErrorDetails);
                var traceId = $"{Activity.Current?.Id}";
                var error = new Error(ErrorType, ErrorTitle, HttpStatusCode, traceId, errorDetails);

                return Results.BadRequest(error);
            }
        }

        return await next.Invoke(context);
    }

    private static ErrorDetails CreateErrorDetails(ValidationFailure failure) =>
        new(failure.ErrorMessage, failure.PropertyName, failure.ErrorCode, failure.Severity.ToString());
}

internal static class ValidadeRouteHandlerBuilder
{
    public static RouteHandlerBuilder Validate<T>(this RouteHandlerBuilder builder)
    { 
        return builder.AddEndpointFilter<ValidationFilter<T>>();
    }
}