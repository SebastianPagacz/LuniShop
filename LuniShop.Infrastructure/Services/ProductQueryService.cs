using LuniShop.Application.Categories.DTO;
using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ProductQueryService(AppDbContext context) : IProductQueryService
{
    public async Task<ProductDto> GetActiveProductByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == id)
            .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image,
                p.ProductCategories
                .Where(pc => pc.Category.IsActive && !pc.Category.IsDeleted)
                .Select(pc => new CategoryDto(pc.CategoryId, pc.Category.Name))
                .AsEnumerable()
            ))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ProductDto>> GetAllActiveProductsAsync(int? categoryId, string? searchTerm, CancellationToken cancellationToken)
    {
        var products = context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted);
        
        if (categoryId.HasValue)
            products.Where(p => p.ProductCategories.Any(pc => pc.CategoryId == categoryId));

        if (searchTerm != null)
            products.Where(p => p.Name.Contains(searchTerm)); // this might be optimization issue to consider

        return await products
                .Select(p => new ProductDto(p.Id, p.Name, p.Description, p.Price, p.Stock, p.Image,
                p.ProductCategories
                .Where(pc => pc.Category.IsActive && !pc.Category.IsDeleted)
                .Select(pc => new CategoryDto(pc.CategoryId, pc.Category.Name))       
            ))
            .ToListAsync(cancellationToken);
    }
}
