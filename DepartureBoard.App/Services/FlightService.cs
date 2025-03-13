using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;

namespace DepartureBoard.App.Services;

public class FlightService(IFlightRepo flightRepo)
{
    private readonly IFlightRepo _flightRepo = flightRepo;
    
    public async Task RegisterFlight(Airplane plane)
    {
        await _flightRepo.AddFlightAsync(plane);
    }
}