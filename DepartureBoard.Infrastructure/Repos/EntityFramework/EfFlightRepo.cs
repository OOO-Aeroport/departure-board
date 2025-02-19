using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfFlightRepo(AppDbContext context) : IFlightRepo
{
    private readonly AppDbContext _context = context;

    public async Task CreateFlightAsync(Plane plane)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            await _context.Planes.AddAsync(plane);
            await _context.SaveChangesAsync();

            var flight = new Flight
            {
                PlaneId = plane.Id,
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