using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public void Add(Product product)
    {
        context.Products.Add(product); // Adding only to EF change tracker
    }

    public async Task<List<Product>> GetAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }
}
