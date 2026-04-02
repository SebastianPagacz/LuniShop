using LuniShop.Application.Products.DTO;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<List<ProductDto>> GetAllActiveItemsAsync(CancellationToken cancellationToken);
    Task<ProductDto> GetActiveItemByIdAsync(int id, CancellationToken cancellationToken);
}
