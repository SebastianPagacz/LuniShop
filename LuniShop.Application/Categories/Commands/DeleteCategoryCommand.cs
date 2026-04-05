using MediatR;

namespace LuniShop.Application.Categories.Commands;

public record DeleteCategoryCommand(int Id) : IRequest<Result<string>> { }