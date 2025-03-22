using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.UseCases;

public class CreateFlightUseCase(IAirplaneFlightUnitOfWork unitOfWork,
    ITicketOfficeClient ticketOffice, IPassengerClient passenger,
    TimeService timeService)
{
    public async Task InvokeAsync(Airplane airplane)
    {
        await unitOfWork.AddAirplaneAndFlightAsync(airplane, timeService.Now);

        if (airplane.Flight == null) throw new NullReferenceException();
        
        var task1 = ticketOffice.NotifyFlightCreated(
            new
            {
                flightId = airplane.Flight.Id, airplaneId = airplane.Id,
                airplane.SeatsAvailable, airplane.BaggageAvailable
            });
        var task2 = passenger.NotifyFlightCreated(
            new
            {
                flightId = airplane.Flight.Id,
                airplaneId = airplane.Id
            });

        await Task.WhenAll(task1, task2);
    }
}