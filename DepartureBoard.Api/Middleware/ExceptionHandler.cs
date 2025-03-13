namespace DepartureBoard.Api.Middleware;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandler> _logger = logger;

    public async Task Invoke(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Request Failed: {Method} {Path}", context.Request.Method, context.Request.Path);
            
            context.Response.StatusCode = 500;
            context.Response.ContentType = "application/json";

            var response = new
            {
                error = "Internal Server Error",
                message = ex.Message,
                stackTrace = ex.StackTrace
            };
            
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}