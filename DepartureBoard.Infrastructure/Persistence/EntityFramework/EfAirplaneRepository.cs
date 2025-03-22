using DepartureBoard.Application.Ports.Persistence;
using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class EfAirplaneRepository(AirplaneFlightDbContext context) : IAirplaneRepository
{
    public async Task AddAsync(Airplane airplane)
        => await context.Airplanes.AddAsync(airplane);
    
    public async Task SaveChangesAsync()
        => await context.SaveChangesAsync();
}