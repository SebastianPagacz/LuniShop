using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public class GetProductByIdHandler(IProductRepository repository) : IRequestHandler<GetProductByIdQuery, Result<Product>>
{
    public async Task<Result<Product>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        var product = await repository.GetByIdAsync(request.Id);

        if (product is null || !product.IsActive)
            return new Result<Product>(false, $"Product with Id: {request.Id} was not found.", null);

        return new Result<Product>(true, null, product);
    }
}
