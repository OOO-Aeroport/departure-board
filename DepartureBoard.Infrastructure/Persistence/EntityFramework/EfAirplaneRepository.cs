using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfAirplaneRepository(AirplaneFlightDbContext context) : IAirplaneRepository
{
    private readonly AirplaneFlightDbContext _context = context;
    
    public async Task AddAsync(Airplane airplane)
        => await _context.Airplanes.AddAsync(airplane);
    
    public async Task SaveChangesAsync()
        => await _context.SaveChangesAsync();
}