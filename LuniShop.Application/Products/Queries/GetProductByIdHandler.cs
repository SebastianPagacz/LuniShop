using LuniShop.Application.Services;
using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetProductByIdHandler(IProductQueryService queryService) : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await queryService.GetActiveProductByIdAsync(request.Id);

        if (product is null) // Validation if product is deleted and/or active is at the QueryService level
            return new Result<Product>(false, $"Product with Id: {request.Id} was not found.", null);

        return new Result<Product>(true, null, product);
    }
}
