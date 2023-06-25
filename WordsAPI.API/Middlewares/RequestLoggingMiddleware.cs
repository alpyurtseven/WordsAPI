using Microsoft.AspNetCore.Http.Extensions;

namespace WordsAPI.API.Middlewares
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _logFilePath;

        public RequestLoggingMiddleware(RequestDelegate next, string logFilePath)
        {
            _next = next;
            _logFilePath = logFilePath;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Gelen isteğin bilgilerini al
            var requestMethod = context.Request.Method;
            var requestPath = context.Request.GetDisplayUrl();
            var requestHeaders = context.Request.Headers;

            // İstek bilgilerini log dosyasına yaz
            var logMessage = $"Method: {requestMethod}, Path: {requestPath}\n";
            logMessage += "Headers:\n";
            foreach (var header in requestHeaders)
            {
                logMessage += $"{header.Key}: {header.Value}\n";
            }
            logMessage += new string('-', 30) + "\n";

            await File.AppendAllTextAsync(_logFilePath, logMessage);

            // Sonraki middleware'e devam et
            await _next(context);
        }
    }
}
