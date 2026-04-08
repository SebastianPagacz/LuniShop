using LuniShop.Application.Products.DTO;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public record GetAllProductsQuery(int? CategoryId, string? SearchTerm) : IRequest<Result<List<ProductDto>>> { }
