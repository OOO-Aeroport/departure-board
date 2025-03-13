using DepartureBoard.Domain.Entities;
using DepartureBoard.Domain.Repos;
using DepartureBoard.Infrastructure.Persistence.EntityFramework;

namespace DepartureBoard.Infrastructure.Repos.EntityFramework;

public class EfAirplaneRepository(AppDbContext context) : IRepository<Airplane>
{
    private readonly AppDbContext _context = context;

    public async Task<int> AddAsync(Airplane airplane)
    {
        await _context.Airplanes.AddAsync(airplane);
        return airplane.Id;
    }

    public async Task<Airplane> GetById(int id)
        => await _context.Airplanes.FindAsync(id) ?? throw new NullReferenceException();
}