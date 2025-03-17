using DepartureBoard.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace DepartureBoard.Infrastructure.Persistence.EntityFramework;

public class AirplaneFlightDbContext(DbContextOptions<AirplaneFlightDbContext> options) : DbContext(options)
{
    public DbSet<Airplane> Airplanes { get; set; }
    public DbSet<Flight> Flights { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<Flight>()
            .HasOne(f => f.Airplane)
            .WithOne(a => a.Flight)
            .HasForeignKey<Flight>(f => f.AirplaneId);
    }
}