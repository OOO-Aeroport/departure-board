namespace DepartureBoard.Domain.Ports.Persistence;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}