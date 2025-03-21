using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.UseCases;

public class ScheduleCheckInUseCase(IFlightRepository flightRepository,
    ICheckInClient checkIn, IPassengerClient passenger,
    TimeService timeService, IServiceLocator factory)
{
    private readonly IFlightRepository _flightRepository = flightRepository;
    private readonly ICheckInClient _checkIn = checkIn;
    private readonly IPassengerClient _passenger = passenger;
    private readonly TimeService _timeService = timeService;
    
    public async Task InvokeAsync(int airplaneId)
    {
        var flight = await _flightRepository.FindByAirplaneIdAsync(airplaneId)
                     ?? throw new NullReferenceException();
        
        var checkInEndTime = _timeService.Now.AddMinutes((int)Constants.CheckInMinuteDuration);
        await _checkIn.NotifyCheckInStart(flight.Id, checkInEndTime);
        await _passenger.NotifyCheckInStart(flight.Id, checkInEndTime);
        
        _ = Task.Run(async () =>
        {
            while (_timeService.Now <= checkInEndTime)
            {
                await Task.Delay(TimeSpan.FromMilliseconds((int)Constants.TickInMs));
            }

            var checkInRenewed = factory.Require<ICheckInClient>() ?? throw new NullReferenceException();
            var passengerRenewed = factory.Require<IPassengerClient>() ?? throw new NullReferenceException();
            await checkInRenewed.NotifyCheckInEnd(flight.Id);
            await passengerRenewed.NotifyCheckInEnd(flight.Id);
        });
    }
}