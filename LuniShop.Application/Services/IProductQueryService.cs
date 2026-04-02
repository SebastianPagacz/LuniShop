using LuniShop.Application.Products.DTO;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<List<ProductDto>> GetAllActiveItemsAsync();
    Task<ProductDto> GetActiveItemByIdAsync(int id);
}
