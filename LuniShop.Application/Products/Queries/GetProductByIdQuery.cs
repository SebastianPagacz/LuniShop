using LuniShop.Application.Products.DTO;
using MediatR;

namespace LuniShop.Application.Products.Queries;

public record GetProductByIdQuery(int Id) : IRequest<Result<ProductDto>> { }
