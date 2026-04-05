using LuniShop.Application.Categories.DTO;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Categories.Commands;

public class UpdateCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<UpdateCategoryCommand, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (existingCategory is null || existingCategory.IsDeleted)
            return new Result<CategoryDto>(false, Message: $"Category with Id: {request.Id} was not found.");

        if (!string.IsNullOrWhiteSpace(request.Name))
            existingCategory.SetName(request.Name);

        if (request.IsActive is false)
            existingCategory.Deactivate();

        if (request.IsActive is true)
            existingCategory.Activate();

        await uow.SaveAsync(cancellationToken);
        var result = new CategoryDto(existingCategory.Id, existingCategory.Name);
        
        return new Result<CategoryDto>(true, Value: result);
    }
}
