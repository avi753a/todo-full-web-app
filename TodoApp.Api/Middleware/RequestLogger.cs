using System.Security.Claims;

namespace TodoApp.Api.Middleware
{
    public class RequestLoggerMiddleware
    {
        private readonly ILogger<RequestLoggerMiddleware> _logger;
        private readonly RequestDelegate _next;
        public RequestLoggerMiddleware(RequestDelegate next, ILogger<RequestLoggerMiddleware> logger)
        {
            _logger = logger;
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            await _next(context);
            var method=context.Request.Method;
            var path=context.Request.Path;
            var username=context.Response.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userId = context.Response.HttpContext.User.FindFirstValue(ClaimTypes.Sid);
            var responceBody=context.Response.Body;
            var responceStatus=context.Response.StatusCode;

            _logger.LogInformation("Method: {Method}, Path: {Path}, Username: {UserName}, UserId: {UserId}, ResponseCode: {ResponseCode}", method, path, username, userId, responceStatus);

        }
    }
}
