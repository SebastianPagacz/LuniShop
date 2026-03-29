using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Commands;

public record DeleteProductCommand(int Id) : IRequest<Result<Product>> { }
