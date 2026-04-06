using LuniShop.Application.Categories.DTO;
using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    public async Task<List<ProductDto>> GetAllActiveItemsAsync(CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image, 
                p.ProductCategories
                .Select(pc => new CategoryDto(pc.CategoryId, pc.Category.Name))
                .AsEnumerable()
            ))
            .ToListAsync(cancellationToken); 
    }

    public async Task<ProductDto> GetActiveItemByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == id)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image,
                p.ProductCategories
                .Select(pc => new CategoryDto(pc.CategoryId, pc.Category.Name))
                .AsEnumerable()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }
}
