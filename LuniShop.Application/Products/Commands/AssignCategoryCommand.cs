using MediatR;

namespace LuniShop.Application.Products.Commands;

public record AssignCategoryCommand(int ProductId, int CategoryId) : IRequest<Result<string>> { }