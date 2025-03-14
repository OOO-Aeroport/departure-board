using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Ports.Persistence;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Adapters.Persistence.EntityFramework;

public class EfAirplaneRepository(AppDbContext context) : IRepository<Airplane>
{
    private readonly AppDbContext _context = context;

    public async Task AddAsync(Airplane airplane)
        => await _context.Airplanes.AddAsync(airplane);

    public async Task<Airplane?> GetByIdAsync(int id)
        => await _context.Airplanes.FindAsync(id);

    public async Task SaveChangesAsync() 
        => await _context.SaveChangesAsync();
}