namespace DepartureBoard.Domain.Repos;

public interface IRepository<T>
{
    Task AddAsync(T entity);
    Task<T?> GetByIdAsync(int id);
    Task SaveChangesAsync();
}