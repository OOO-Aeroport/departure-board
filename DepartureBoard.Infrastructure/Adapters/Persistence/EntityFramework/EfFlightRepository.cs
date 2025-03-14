using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Adapters.Persistence.EntityFramework;

public class EfFlightRepository(AppDbContext context) : IRepository<Flight>
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Flight flight)
        => await _context.Flights.AddAsync(flight);

    public async Task<Flight?> GetByIdAsync(int id)
        => await _context.Flights.FindAsync(id);
    
    public async Task SaveChangesAsync() 
        => await _context.SaveChangesAsync();
}