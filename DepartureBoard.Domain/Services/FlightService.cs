using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;

namespace DepartureBoard.Domain.Services;

public class FlightService(IFlightRepo repo)
{
    private readonly IFlightRepo _repo = repo;
    
    public async Task CreateFlightAsync(Plane plane)
    {
        await _repo.CreateFlightAsync(plane);
    }
}