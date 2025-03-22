namespace DepartureBoard.Api.Middleware;

public class RequestLogger(RequestDelegate next, ILogger<RequestLogger> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        logger.LogInformation("Request: {Method} {Path}", context.Request.Method, context.Request.Path);
        
        await next(context);
        
        logger.LogInformation("Request completed: {Method} {Path}", context.Request.Method, context.Request.Path);
    }
}