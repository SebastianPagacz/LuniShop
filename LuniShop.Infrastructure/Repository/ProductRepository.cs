using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Repository;

public class ProductRepository(AppDbContext context) : IRepository<Product>
{
    public void Add(Product item)
    {
        context.Products.Add(item); // Adding only to EF change tracker
    }

    public async Task<List<Product>> GetAsync(CancellationToken cancellationToken)
    {
        return await context.Products.ToListAsync(cancellationToken);
    }

    public async Task<Product> GetByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Products.FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }
}
