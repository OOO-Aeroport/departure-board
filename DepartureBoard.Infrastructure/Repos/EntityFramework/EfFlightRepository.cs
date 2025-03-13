using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfFlightRepository(AppDbContext context) : IRepository<Flight>
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Flight flight)
        => await _context.Flights.AddAsync(flight);

    public async Task<Flight?> GetByIdAsync(int id)
        => await _context.Flights.FindAsync(id);
}