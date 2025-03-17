using DepartureBoard.Application.Ports.Network;
using DepartureBoard.Application.Ports.Network.Factories;
using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Application.Services;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.UseCases;

public class ScheduleCheckInUseCase(IFlightRepository flightRepository,
    ICheckInClient checkIn, TimeService timeService, ICheckInClientFactory factory)
{
    private readonly IFlightRepository _flightRepository = flightRepository;
    private readonly ICheckInClient _checkIn = checkIn;
    private readonly TimeService _timeService = timeService;
    
    public async Task InvokeAsync(int airplaneId)
    {
        var flight = await _flightRepository.FindByAirplaneIdAsync(airplaneId)
                     ?? throw new NullReferenceException();
        
        var checkInEndTime = _timeService.Now.AddMinutes((double)Constants.CheckInMinuteDuration);
        await _checkIn.NotifyRegistrationStart(flight.Id, checkInEndTime);
        
        _ = Task.Run(async () =>
        {
            while (_timeService.Now <= checkInEndTime)
            {
                await Task.Delay(TimeSpan.FromMilliseconds((int)Constants.TickInMilliseconds));
            }

            var checkInRenewed = factory.Require() ?? throw new NullReferenceException();
            await checkInRenewed.NotifyRegistrationEnd(flight.Id);
        });
    }
}