using FluentValidation;
using System.Net;
using System.Text.Json;

namespace SalesManagementSystem.API.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = "Internal Server Error.";
            string errors = null;

            switch (exception)
            {
                case ValidationException validationException:
                    statusCode = HttpStatusCode.BadRequest;
                    message = "Validation Failed.";
                    errors = JsonSerializer.Serialize(validationException.Errors.Select(e => new { e.PropertyName, e.ErrorMessage }));
                    break;

                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    break;
            }

            context.Response.StatusCode = (int)statusCode;

            var result = JsonSerializer.Serialize(new
            {
                message = message,
                errors = errors ?? exception.Message
            });

            return context.Response.WriteAsync(result);
        }
    }
}