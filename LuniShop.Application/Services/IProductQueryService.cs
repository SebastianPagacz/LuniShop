using LuniShop.Application.Products.DTO;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<ProductDto> GetActiveProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ProductDto>> GetAllActiveProductsAsync(int? categoryId, string? searchTerm, CancellationToken cancellationToken);
}
