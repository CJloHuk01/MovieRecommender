using Microsoft.IdentityModel.Tokens;
using System.Net;
using System.Text.Json;

namespace MovieRecommender
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Произошло необработанное исключение");
                await HandleExceptionAsync(context, ex);
            }
        }

        private static Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            if (exception is SecurityTokenException)
            {
                context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
            }
            else
            {
                context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
            }

            var response = new
            {
                error = "Произошла ошибка",
                message = exception.Message,
                statusCode = context.Response.StatusCode,
                timestamp = DateTime.UtcNow
            };

            var json = JsonSerializer.Serialize(response, new JsonSerializerOptions
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase
            });

            return context.Response.WriteAsync(json);
        }
    }
}
