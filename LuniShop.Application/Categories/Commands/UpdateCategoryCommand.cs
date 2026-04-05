using LuniShop.Application.Categories.DTO;
using MediatR;

namespace LuniShop.Application.Categories.Commands;

public record UpdateCategoryCommand(int Id, string? Name, bool? IsActive) : IRequest<Result<CategoryDto>> { }