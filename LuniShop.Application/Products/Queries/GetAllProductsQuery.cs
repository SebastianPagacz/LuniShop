using LuniShop.Application.Products.DTO;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public record GetAllProductsQuery : IRequest<Result<List<ProductDto>>> { }
