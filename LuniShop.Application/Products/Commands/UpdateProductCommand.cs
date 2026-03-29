using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public record UpdateProductCommand(int Id, string? Name, string? Description, decimal? Price, int? Stock, string? Image, bool? IsActive) : IRequest<Result<Product>> { }
