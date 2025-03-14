namespace DepartureBoard.Api.Middleware;

public class RequestLogger(RequestDelegate next, ILogger<RequestLogger> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<RequestLogger> _logger = logger;

    public async Task InvokeAsync(HttpContext context)
    {
        _logger.LogInformation("({Now}) Request: {Method} {Path}", DateTime.Now, context.Request.Method, context.Request.Path);
        
        await _next(context);
        
        _logger.LogInformation("({Now}) Request Completed: {Method} {Path}", DateTime.Now, context.Request.Method, context.Request.Path);
    }
}