using DepartureBoard.Domain.Entities;

namespace DepartureBoard.Application.Ports.Persistence;

public interface IAirplaneRepository
{
    Task AddAsync(Airplane airplane);
}