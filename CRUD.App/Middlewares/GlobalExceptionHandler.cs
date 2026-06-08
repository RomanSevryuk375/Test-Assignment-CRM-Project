using System.ComponentModel.DataAnnotations;

namespace CRUD.App.Middlewares;

public class GlobalExceptionHandler(RequestDelegate next)
{
    public async Task Invoke(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception ex)
        {
            await HandleException(context, ex);
        }
    }

    public static Task HandleException(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var statusCode = exception switch
        {
            FluentValidation.ValidationException => StatusCodes.Status400BadRequest,
            ValidationException => StatusCodes.Status400BadRequest,
            ArgumentException => StatusCodes.Status400BadRequest,
            KeyNotFoundException => StatusCodes.Status404NotFound,
            UnauthorizedAccessException => StatusCodes.Status401Unauthorized,

            _ => StatusCodes.Status500InternalServerError
        };

        context.Response.StatusCode = statusCode;

        if (exception is FluentValidation.ValidationException fluentException)
        {
            var errors = fluentException.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );

            var validationResponse = new
            {
                StatusCode = statusCode,
                Message = "One or more validation failures occurred.",
                Errors = errors
            };

            return context.Response.WriteAsJsonAsync(validationResponse);
        }

        var standardResponse = new
        {
            StatusCode = statusCode,
            Message = exception.Message
        };

        return context.Response.WriteAsJsonAsync(standardResponse);
    }
}

public static class UseMiddleware
{
    public static IApplicationBuilder UseGlobalExceptionHandler(this IApplicationBuilder builder)
    {
        return builder.UseMiddleware<GlobalExceptionHandler>();
    }
}
