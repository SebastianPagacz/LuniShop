using LuniShop.Application.Reviews.DTO;
using MediatR;

namespace LuniShop.Application.Reviews.Queries;

public record GetAllReviewsByProductIdQuery(int productId) : IRequest<Result<List<ReviewDto>>> { }
