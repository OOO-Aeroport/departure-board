using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfFlightRepository(AirplaneFlightDbContext context) : IFlightRepository
{
    public async Task AddAsync(Flight flight)
        => await context.Flights.AddAsync(flight);
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
    
    public async Task<Flight?> FindByAirplaneIdAsync(int airplaneId)
        => await context.Flights.FirstOrDefaultAsync(f => f.AirplaneId == airplaneId);
}