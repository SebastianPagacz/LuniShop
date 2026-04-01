using LuniShop.Domain.Models;

namespace LuniShop.Domain.Repository;

public interface IRepository<T> where T : class
{
    void Add(T item);
    Task<T> GetByIdAsync(int id);
    Task<List<T>> GetAsync();
}
