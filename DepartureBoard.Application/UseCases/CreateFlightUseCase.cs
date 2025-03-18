using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.UseCases;

public class CreateFlightUseCase(IAirplaneFlightUnitOfWork unitOfWork,
    ITicketOfficeClient ticketOffice, IPassengerClient passenger,
    TimeService timeService)
{
    private readonly IAirplaneFlightUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITicketOfficeClient _ticketOffice = ticketOffice;
    private readonly IPassengerClient _passenger = passenger;

    public async Task InvokeAsync(Airplane airplane)
    {
        await _unitOfWork.AddAirplaneAndFlightAsync(airplane, timeService.Now);

        if (airplane.Flight == null) throw new NullReferenceException();
        
        var task1 = _ticketOffice.NotifyFlightCreated(
            new
            {
                flightId = airplane.Flight.Id, airplaneId = airplane.Id,
                airplane.SeatsAvailable, airplane.BaggageAvailable
            });
        var task2 = _passenger.NotifyFlightCreated(
            new
            {
                flightId = airplane.Flight.Id,
                airplaneId = airplane.Id
            });

        await Task.WhenAll(task1, task2);
    }
}