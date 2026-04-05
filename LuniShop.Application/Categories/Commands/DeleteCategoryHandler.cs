using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Categories.Commands;

public class DeleteCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<DeleteCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteCategoryCommand request, CancellationToken cancellationToken)
    {
        var existingCategory = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (existingCategory is null || existingCategory.IsDeleted || !existingCategory.IsActive)
            return new Result<string>(false, Message: $"Category with Id: {request.Id} was not found");

        existingCategory.Delete();
        await uow.SaveAsync(cancellationToken);

        return new Result<string>(true);
    }
}
