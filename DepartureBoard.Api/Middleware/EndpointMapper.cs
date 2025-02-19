namespace DepartureBoard.Api.Middleware;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapGet("", async () =>
        {
            await Task.Delay(5000);
        });
    }
}