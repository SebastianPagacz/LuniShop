using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public record GetAllProductsQuery : IRequest<Result<List<Product>>> { }
