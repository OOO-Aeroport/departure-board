namespace DepartureBoard.Domain.Repos;

public interface IRepository<T>
{
    Task<int> AddAsync(T entity);
    Task<T> GetById(int id);
}