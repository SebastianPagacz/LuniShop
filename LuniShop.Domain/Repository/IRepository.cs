using LuniShop.Domain.Models;

namespace LuniShop.Domain.Repository;

public interface IRepository<T> where T : class
{
    void Add(T item);
    Task<T> GetByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<T>> GetAsync(CancellationToken cancellationToken);
}
