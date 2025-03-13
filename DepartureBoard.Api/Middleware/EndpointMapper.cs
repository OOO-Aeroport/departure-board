using DepartureBoard.App.Services;
using DepartureBoard.Domain.Entities;
using Microsoft.AspNetCore.Http.HttpResults;

namespace DepartureBoard.Api.Middleware;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPut("departure-board/time/speed-factor", (HttpContext context, TimeService timeService) =>
        {
            if (!context.Request.Query.TryGetValue("speed-factor", out var temp))
                return Results.BadRequest("speed-factor not found");

            if (!int.TryParse(temp, out var speedFactor))
                return Results.BadRequest("speed-factor is invalid");
            
            timeService.SpeedFactor = speedFactor;
            
            return Results.Ok();
        });

        app.MapGet("departure-board/time", (TimeService timeService)
            => Results.Ok(timeService.Now.ToString("HH:mm")));
        
        app.MapPost("departure-board/planes", async (FlightService flightService,
            TimeService timeService, Airplane plane) =>
        {
            var registrationEndTime = (timeService.Now + TimeSpan.FromHours(2)).ToString("HH:mm");
            //await flightService.RegisterFlight(plane, registrationEndTime);
            return Results.Ok();
        });
    }
}