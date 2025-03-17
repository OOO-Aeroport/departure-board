using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfFlightRepository(AirplaneFlightDbContext context) : IFlightRepository
{
    private readonly AirplaneFlightDbContext _context = context;
    
    public async Task AddAsync(Flight flight)
        => await _context.Flights.AddAsync(flight);
    
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
    
    public async Task<Flight?> FindByAirplaneIdAsync(int airplaneId)
        => await _context.Flights.FirstOrDefaultAsync(f => f.AirplaneId == airplaneId);
}