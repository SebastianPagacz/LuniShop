using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Categories.Commands;

public class AddCategoryHandler(IRepository<Category> repository, IUnitOfWork uow) : IRequestHandler<AddCategoryCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddCategoryCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.Name))
            return new Result<string>(false, Message : $"Name can't be null or empty.");

        var newCategory = Category.Create(request.Name);
        repository.Add(newCategory);
        await uow.SaveAsync(cancellationToken);

        return new Result<string>(true,Message : $"Category with Id: {newCategory.Id} has been created.", Value: $"api/Categories/{newCategory.Id}");
    }
}
