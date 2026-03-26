using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<Result<Product>> { }
