using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using LuniShop.Domain.Models;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    public async Task<List<ProductDto>> GetAllActiveProductsAsync()
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image))
            .ToListAsync(); 
    }

    public async Task<ProductDto> GetActiveProductByIdAsync(int id)
    {
        return await context.Products
            .AsNoTracking().
            Where(p => p.IsActive && !p.IsDeleted).
            Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image)).
            FirstOrDefaultAsync(p => p.Id == id);
    }
}
