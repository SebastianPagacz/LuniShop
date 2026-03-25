using LuniShop.Domain.Models;

namespace LuniShop.Domain.Repository;

public interface IProductRepository
{
    Task AddAsync(Product product);
    Task<Product> GetByIdAsync(int id);
    Task<List<Product>> GetAsync();
    void UpdateAsync(Product product);
}
