using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public class DeleteProductHandler(IUnitOfWork uow, IRepository<Product> repository) : IRequestHandler<DeleteProductCommand, Result<Product>>
{
    public async Task<Result<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (existingProduct.IsDeleted || existingProduct is null)
            return new Result<Product>(false, $"Product with Id: {request.Id} was not found.", null);

        existingProduct.Delete();

        await uow.SaveAsync(cancellationToken);

        return new Result<Product>(true, $"Product with Id: {request.Id} has been deleted.", null);
    }
}
