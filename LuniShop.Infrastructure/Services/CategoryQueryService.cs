using LuniShop.Application.Categories.DTO;
using LuniShop.Application.Services;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using System.Collections.Immutable;

namespace LuniShop.Infrastructure.Services;

public class CategoryQueryService(AppDbContext context) : ICategoryQueryService
{
    public async Task<CategoryDto> GetActiveCategoryByIdAsync(int id, CancellationToken cancellationToken)
    {
        return await context.Categories
            .AsNoTracking()
            .Where(c => !c.IsDeleted && c.IsActive)
            .Select(c => new CategoryDto(c.Id, c.Name))
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }
}
