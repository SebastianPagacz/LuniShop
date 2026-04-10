namespace UserService.Domain.Repository;

public interface IRepository<T> where T : class
{
    void Add(T item);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
}