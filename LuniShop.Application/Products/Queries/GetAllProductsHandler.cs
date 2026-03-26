using LuniShop.Application.Services;
using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetAllProductsHandler(IProductQueryService queryService) : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
{
    public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await queryService.GetAllActiveProductsAsync();

        return new Result<List<Product>>(true, null, products.Where(p => p.IsActive).ToList()); // Future DTO mapping
    }
}
