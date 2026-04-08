using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetAllProductsHandler(IProductQueryService productQueryService, ICategoryQueryService categoryQueryService) : IRequestHandler<GetAllProductsQuery, Result<List<ProductDto>>>
{
    public async Task<Result<List<ProductDto>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        if (request.CategoryId.HasValue)
        {
            var existingCategory = await categoryQueryService.GetActiveCategoryByIdAsync((int)request.CategoryId, cancellationToken);

            if (existingCategory is null)
                return new Result<List<ProductDto>>(false, Message: $"Category with Id: {request.CategoryId} was not found.");
        }

        var products = await productQueryService.GetAllActiveProductsAsync(request.CategoryId, request.SearchTerm, cancellationToken);

        return new Result<List<ProductDto>>(true, Value: products);
    }
}
