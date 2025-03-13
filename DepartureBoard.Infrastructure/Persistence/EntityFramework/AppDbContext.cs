using DepartureBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<Flight> Flights { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Airplane>().ToTable("planes");
        builder.Entity<Flight>().ToTable("flights");
    }
}