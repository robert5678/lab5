using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace CookieExample.Middleware
{
    public class ExceptionLoggingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await LogExceptionAsync(ex);
                throw;
            }
        }

        private async Task LogExceptionAsync(Exception ex)
        {
            var logFilePath = Path.Combine(Directory.GetCurrentDirectory(), "Logs", "errors.log");
            Directory.CreateDirectory(Path.GetDirectoryName(logFilePath)!);

            var logMessage = $"{DateTime.UtcNow}: {ex.Message}{Environment.NewLine}{ex.StackTrace}{Environment.NewLine}";
            await File.AppendAllTextAsync(logFilePath, logMessage);
        }
    }
}
