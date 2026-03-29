using LuniShop.Application.Services;
using LuniShop.Domain.Models;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    public async Task<List<Product>> GetAllActiveProductsAsync()
    {
        return await context.Products.AsNoTracking().Where(p => p.IsActive && !p.IsDeleted).ToListAsync(); // ToDo: Future DTO implementation
    }

    public async Task<Product> GetActiveProductByIdAsync(int id)
    {
        return await context.Products.AsNoTracking().Where(p => p.IsActive && !p.IsDeleted).FirstOrDefaultAsync(p => p.Id == id); // ToDo: Future DTO implementation
    }
}
