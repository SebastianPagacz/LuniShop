using LuniShop.Domain.Exceptions;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IProductRepository
{
    public async Task AddAsync(Product product)
    {
        await context.Products.AddAsync(product);
        await context.SaveChangesAsync();
    }

    public async Task<List<Product>> GetAsync()
    {
        return await context.Products.ToListAsync();
    }

    public async Task<Product> GetByIdAsync(int id)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id);
    }

    public void UpdateAsync(Product product)
    {
        context.SaveChanges(); // Why not to make it async? I could also check if the operation was completed Task.CompletedTask
    }
}
