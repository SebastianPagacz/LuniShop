using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetAllProductsHandler(IProductQueryService queryService) : IRequestHandler<GetAllProductsQuery, Result<List<ProductDto>>>
{
    public async Task<Result<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await queryService.GetAllActiveItemsAsync();

        return new Result<List<ProductDto>>(true, null, products); 
    }
}
