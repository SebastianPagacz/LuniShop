using LuniShop.Application.Categories.DTO;
using MediatR;

namespace LuniShop.Application.Categories.Queries;

public record GetCategoryByIdQuery(int Id) : IRequest<Result<CategoryDto>> { }
