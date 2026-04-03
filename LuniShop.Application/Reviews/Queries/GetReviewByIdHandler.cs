using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Reviews.Queries;

public class GetReviewByIdHandler(IReviewQueryService queryService) : IRequestHandler<GetReviewByIdQuery, Result<ReviewDto>>
{
    public async Task<Result<ReviewDto>> Handle(GetReviewByIdQuery request, CancellationToken cancellationToken)
    {
        var existingReview = await queryService.GetActiveReviewByIdAsync(request.ProductId, request.ReviewId, cancellationToken);

        if (existingReview is null)
            return new Result<ReviewDto>(false, Message : $"Review with Id: {request.ReviewId} was not found.");

        return new Result<ReviewDto>(true, Value : existingReview);
    }
}
