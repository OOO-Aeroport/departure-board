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
            IGroundHandlingClient groundHandling, RegisterFlightScenario scenario,
            TimeService timeService, Airplane airplane) =>
        {
            var departureTime = timeService.Now + TimeSpan.FromHours(4);
            
            await scenario.Invoke(airplane, departureTime,
                ticketOffice, groundHandling);

            return Results.Ok();
        });

        app.MapPut("departure-board/planes/{id}/handled", (int id, HandleAirplaneScenario scenario)
            => scenario.Invoke(id));

        app.MapPost("departure-board/flights/{id}/passengers", async (int id, List<object> passengers,
            SendPassengersToBoardScenario scenario) 
            => await scenario.Invoke(id, passengers));
    }
}