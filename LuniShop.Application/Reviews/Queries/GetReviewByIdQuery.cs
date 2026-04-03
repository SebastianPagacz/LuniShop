using LuniShop.Application.Reviews.DTO;
using MediatR;

namespace LuniShop.Application.Reviews.Queries;

public record GetReviewByIdQuery(int ProductId, int ReviewId) : IRequest<Result<ReviewDto>> { }
