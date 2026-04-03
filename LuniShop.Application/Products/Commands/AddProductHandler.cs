using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public class AddProductHandler(IRepository<Product> repository, IUnitOfWork uow) : IRequestHandler<AddProductCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddProductCommand request, CancellationToken cancellationToken)
    {
        var newProduct = Product.Create(request.Name, request.Price);

        if(request.Stock.HasValue)
            newProduct.SetStock(request.Stock.Value);

        if (!string.IsNullOrEmpty(request.Description))
            newProduct.SetDescription(request.Description);

        if (!string.IsNullOrEmpty(request.Image))
            newProduct.SetImage(request.Image);

        if(request.IsActive is not null && request.IsActive == true) // by default new Product.IsActive = flase, if it has value it should be set to active otherwise left as default
            newProduct.Activate();

        repository.Add(newProduct);
        await uow.SaveAsync();

        return new Result<string>(true, $"Product with Id: {newProduct.Id} was created.", $"api/Products/{newProduct.Id}");
    }
}
