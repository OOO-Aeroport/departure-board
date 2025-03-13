using DepartureBoard.App.Services;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Infrastructure.ExternalApi;

namespace DepartureBoard.Api.Middleware;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPut("departure-board/time/speed-factor", (HttpContext context,
            TimeService timeService) =>
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
        
        app.MapPost("departure-board/planes", async (TicketOfficeApi ticketOfficeApi,
            GroundHandlingApi groundHandlingApi, FlightService flightService,
            TimeService timeService, Airplane airplane) =>
        {
            var departureTime = timeService.Now + TimeSpan.FromHours(4);
            await flightService.RegisterFlight(airplane, departureTime);
            await ticketOfficeApi.Post(new
            {
                airplane.Flight.Id,
                departureTime,
                airplane.SeatsAvailable,
                airplane.BaggageAvailable
            });

            await groundHandlingApi.Post(new
            {
                airplane.Id,
                airplane.Gate,
                airplane.CurrentFuel,
                airplane.MaxFuel
            });
            
            return Results.Ok();
        });
    }
}