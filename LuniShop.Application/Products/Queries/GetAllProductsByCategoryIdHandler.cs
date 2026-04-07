using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetAllProductsByCategoryIdHandler(IProductQueryService productQuery, ICategoryQueryService categoryQuery) : IRequestHandler<GetAllProductsByCategoryIdQuery, Result<List<ProductDto>>>
{
    public async Task<Result<List<ProductDto>>> Handle(GetAllProductsByCategoryIdQuery request, CancellationToken cancellationToken)
    {
        var existingCategory = await categoryQuery.GetActiveCategoryByIdAsync(request.CategoryId, cancellationToken);

        if (existingCategory is null)
            return new Result<List<ProductDto>>(false, Message: $"Category with Id: {request.CategoryId} was not found.");

        var products = await productQuery.GetAllActiveProductsByCategoryIdAsync(request.CategoryId, cancellationToken);

        return new Result<List<ProductDto>>(true, Value: products);
    }
}