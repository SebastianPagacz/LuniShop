using LuniShop.Application.Products.DTO;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<List<ProductDto>> GetAllActiveProductsAsync(CancellationToken cancellationToken);
    Task<ProductDto> GetActiveProductByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ProductDto>> GetAllActiveProductsByCategoryIdAsync(int categoryId, CancellationToken cancellationToken);
}
