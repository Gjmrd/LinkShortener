using Microsoft.AspNetCore.Http;
using System;
using System.Threading.Tasks;

namespace LinkShortener.ExceptionHandlers
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlerMiddleware(RequestDelegate next)
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
                await HandleUnknownError(httpContext, ex);
            }
        }

        private static Task HandleUnknownError(HttpContext httpContext, Exception ex)
        {
            httpContext.Response.ContentType = "application/json"; //ToDo: replace it!
            httpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            return httpContext.Response.WriteAsync(ex.Message);
        }
    }
}
