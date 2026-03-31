using LuniShop.Application.Products.DTO;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<List<ProductDto>> GetAllActiveProductsAsync();
    Task<ProductDto> GetActiveProductByIdAsync(int id);
}
