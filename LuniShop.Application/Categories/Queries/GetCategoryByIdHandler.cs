using LuniShop.Application.Categories.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Categories.Queries;

public class GetCategoryByIdHandler(ICategoryQueryService queryService) : IRequestHandler<GetCategoryByIdQuery, Result<CategoryDto>>
{
    public async Task<Result<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
    {
        var existingCategory = await queryService.GetActiveCategoryByIdAsync(request.Id, cancellationToken);

        if (existingCategory is null)
            return new Result<CategoryDto>(false, Message: $"Category with Id: {request.Id} was not found.");

        return new Result<CategoryDto>(true, Value: existingCategory);
    }
}
