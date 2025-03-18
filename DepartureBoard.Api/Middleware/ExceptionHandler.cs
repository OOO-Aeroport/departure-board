namespace DepartureBoard.Api.Middleware;

public class ExceptionHandler(RequestDelegate next, ILogger<ExceptionHandler> logger)
{
    private readonly RequestDelegate _next = next;
    private readonly ILogger<ExceptionHandler> _logger = logger;
    
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "{Message}", ex.Message);
            var response = new
            {
                StatusCode = 500,
                Error = ex.GetType().Name,
                Message = ex.Message,
            };
            
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(response);
        }
    }
}