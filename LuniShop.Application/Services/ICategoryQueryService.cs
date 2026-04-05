using LuniShop.Application.Categories.DTO;

namespace LuniShop.Application.Services;

public interface ICategoryQueryService
{
    Task<CategoryDto> GetActiveCategoryByIdAsync(int id, CancellationToken cancellationToken);
}
