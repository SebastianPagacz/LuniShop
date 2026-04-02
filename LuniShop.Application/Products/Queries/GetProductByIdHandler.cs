using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetProductByIdHandler(IProductQueryService queryService) : IRequestHandler<GetProductByIdQuery, Result<ProductDto>>
{
    public async Task<Result<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await queryService.GetActiveItemByIdAsync(request.Id);

        if (product is null) // Validation if product is deleted and/or active is at the QueryService level
            return new Result<ProductDto>(false, $"Product with Id: {request.Id} was not found.", null);

        return new Result<ProductDto>(true, null, product);
    }
}
