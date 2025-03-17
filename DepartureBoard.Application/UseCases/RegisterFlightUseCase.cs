using DepartureBoard.Application.Dto;
using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.UseCases;

public class RegisterFlightUseCase(IAirplaneFlightUnitOfWork unitOfWork,
    ITicketOfficeClient ticketOffice, IPassengerClient passenger,
    TimeService timeService)
{
    private readonly IAirplaneFlightUnitOfWork _unitOfWork = unitOfWork;
    private readonly ITicketOfficeClient _ticketOffice = ticketOffice;
    private readonly IPassengerClient _passenger = passenger;

    public async Task InvokeAsync(Airplane airplane)
    {
        await _unitOfWork.AddAirplaneAndFlightAsync(airplane, timeService.Now);
        
        var task1 = _ticketOffice.Post(new FlightDto(airplane.Flight!.Id, airplane.Id,
            airplane.SeatsAvailable, airplane.BaggageAvailable));
        var task2 = _passenger.Post(new { airplane.Id });

        await Task.WhenAll(task1, task2);
    }
}