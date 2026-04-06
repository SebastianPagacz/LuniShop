using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public class AssignCategoryHandler(IRepository<Product> repository, ICategoryQueryService queryService, IUnitOfWork uow) : IRequestHandler<AssignCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AssignCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.ProductId, cancellationToken);

        if (existingProduct is null || existingProduct.IsDeleted || !existingProduct.IsActive)
            return new Result<string>(false, Message: $"Product with Id: {request.ProductId} was not found.");

        var existingCategory = await queryService.GetActiveCategoryByIdAsync(request.CategoryId, cancellationToken);

        if (existingCategory is null)
            return new Result<string>(false, Message: $"Category with Id: {request.CategoryId} was not found.");

        existingProduct.AssignCategory(existingCategory.Id);
        await uow.SaveAsync(cancellationToken);

        return new Result<string>(true, Message: $"Category with Id: {existingCategory.Id} has been assigned to product with Id: {existingProduct.Id}.", Value: $"api/Products/{existingProduct.Id}");
    }
}