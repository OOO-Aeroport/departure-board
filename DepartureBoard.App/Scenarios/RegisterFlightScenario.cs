using DepartureBoard.App.Ports.Network;
using DepartureBoard.App.Ports.Persistence;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.App.Scenarios;

public class RegisterFlightScenario(IAirplaneAndFlightUnitOfWork unitOfWork,
    ITicketOfficeClient ticketOffice, IGroundHandlingClient groundHandling)
{
    private readonly IAirplaneAndFlightUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITicketOfficeClient _ticketOffice = ticketOffice;
    private readonly IGroundHandlingClient _groundHandling = groundHandling;
    
    public async Task Invoke(Airplane airplane, DateTime departureTime,
        ITicketOfficeClient ticketOffice, IGroundHandlingClient groundHandling)
    {
        await _unitOfWork.AddAirplaneAndFlightAsync(airplane, departureTime);
        
        _ = ticketOffice.Post(new
        {
            airplane.Flight!.Id,
            departureTime,
            airplane.SeatsAvailable,
            airplane.BaggageAvailable
        });

        _ = groundHandling.Post(new
        {
            airplane.Id,
            airplane.CurrentFuel,
            airplane.MaxFuel
        });
    }
}