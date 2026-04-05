using MediatR;

namespace LuniShop.Application.Categories.Commands;

public record AddCategoryCommand(string Name) : IRequest<Result<string>> { }
