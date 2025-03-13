using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfFlightRepository(AppDbContext context) : IRepository<Flight>
{
    private readonly AppDbContext _context = context;

    public async Task AddFlightAsync(Airplane airplane)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.Airplanes.AddAsync(airplane);
            await _context.SaveChangesAsync();

            var flight = new Flight
            {
                PlaneId = airplane.Id,
                PassengersCount = 0
            };

            await _context.Flights.AddAsync(flight);
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
        }
    }
}