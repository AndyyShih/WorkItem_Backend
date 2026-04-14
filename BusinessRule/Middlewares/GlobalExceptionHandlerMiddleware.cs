using Common.Enums;
using DataAccess.Models.ResponseModel;
using Microsoft.AspNetCore.Http;
using Serilog;
using System.Text.Json;

namespace BusinessRule.Middleware
{
    public class GlobalExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlerMiddleware(Microsoft.AspNetCore.Http.RequestDelegate next)
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

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            ApiStatusCode statusCode = ApiStatusCode.InternalServerError;
            var errors = new List<string> { exception.Message };
            var message = "An unexpected error occurred.";

            switch (exception)
            {
                case ArgumentException _:
                    /*
                case FluentValidation.ValidationException _:
                    statusCode = ApiStatusCode.BadRequest;
                    message = "Invalid request data.";
                    break;
                    */
                case KeyNotFoundException _:
                    statusCode = ApiStatusCode.NotFound;
                    message = "Resource not found.";
                    break;
                case UnauthorizedAccessException _:
                    statusCode = ApiStatusCode.NotFound;
                    message = "Resource not found.";
                    break;
                    // 可以根據需要添加更多的異常類型處理
            }

            Log.Error(exception, "An error occurred: {Message}", exception.Message);

            var response = ApiResponse<string>.Create(statusCode, message: message, errors: errors);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;
            return context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }
    }
}
