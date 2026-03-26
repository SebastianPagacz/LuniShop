using LuniShop.Domain.Models;

namespace LuniShop.Application.Services;

public interface IProductQueryService
{
    Task<List<Product>> GetAllActiveProductsAsync();
}
