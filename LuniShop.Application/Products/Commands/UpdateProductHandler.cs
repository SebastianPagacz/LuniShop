using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public class UpdateProductHandler(IUnitOfWork uow, IRepository<Product> repository) : IRequestHandler<UpdateProductCommand, Result<Product>>
{
    public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (existingProduct.IsDeleted || existingProduct is null)
            return new Result<Product>(false, $"Product with Id: {request.Id} was not found", null);

        if(!string.IsNullOrEmpty(request.Name))
            existingProduct.SetName(request.Name);

        if(request.Description != null) // Can be empty but can't be null
            existingProduct.SetDescription(request.Description);

        if (request.Price != null)
            existingProduct.SetPrice((decimal)request.Price);

        if(request.Stock != null)
            existingProduct.SetStock((int)request.Stock);

        if (request.Image != null) // Can be empty but can't be null
            existingProduct.SetDescription(request.Image);

        if (request.IsActive != null && existingProduct.IsActive)
            existingProduct.Deactivate();

        if (request.IsActive != null && !existingProduct.IsActive)
            existingProduct.Activate();

        existingProduct.Update();

        await uow.SaveAsync();

        return new Result<Product>(true, null, existingProduct);
    }
}
