using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public record AddProductCommand(string Name, string? Description, decimal Price, int? Stock, string? Image, bool? IsActive) : IRequest<Result<Product>>
{
}
