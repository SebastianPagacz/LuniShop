using LuniShop.Domain.Models;

namespace LuniShop.Domain.Repository;

public interface IRepository
{
    void Add(Product product);
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetAsync();
}
