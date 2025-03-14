using DepartureBoard.App.Ports.Network;
using DepartureBoard.App.Scenarios;
using DepartureBoard.Domain.Entities;
using DepartureBoard.Misc;

namespace DepartureBoard.Api;

public static class EndpointMapper
{
    public static void MapEndpoints(this WebApplication app)
    {
        app.MapPut("departure-board/time/speed-factor", (HttpContext context,
            TimeService timeService) =>
        {
            if (!context.Request.Query.TryGetValue("speed-factor", out var temp)
                || !int.TryParse(temp, out var speedFactor))
            {
                return Results.BadRequest("speed-factor is invalid");
            }

            timeService.SpeedFactor = speedFactor;
            
            return Results.Ok();
        });

        app.MapGet("departure-board/time", (HttpContext context, TimeService timeService) =>
        {
            /*var format = context.Request.Query.TryGetValue("days", out var temp)
                && bool.TryParse(temp, out var days)
                && days
                ? "dd:HH:mm"
                : "HH:mm";

            return Results.Ok(timeService.Now.ToString(format));*/
            return Results.Ok(timeService.Now);
        });

        app.MapPost("departure-board/planes", async (ITicketOfficeClient ticketOffice,
            IGroundHandlingClient groundHandling, RegisterFlightScenario flightService,
            TimeService timeService, Airplane airplane) =>
        {
            var departureTime = timeService.Now + TimeSpan.FromHours(4);
            
            await flightService.Invoke(airplane, departureTime,
                ticketOffice, groundHandling);

            return Results.Ok();
        });

        app.MapPost("departure-board/planes/handled/{id}", (int id, MarkAirplaneAsHandledScenario service)
            => service.Invoke(id));

        app.MapPost("departure-board/flights/passengers", (int flightId, List<object> passengers,
            ILogger<Program> logger, SendPassengersToBoardScenario scenario) =>
        {
            _ = scenario.Invoke(flightId, passengers);
        });
    }
}