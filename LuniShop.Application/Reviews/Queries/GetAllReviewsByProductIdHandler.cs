using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using MediatR;

namespace LuniShop.Application.Reviews.Queries;

public class GetAllReviewsByProductIdHandler(IReviewQueryService reviewQuery) : IRequestHandler<GetAllReviewsByProductIdQuery, Result<List<ReviewDto>>>
{
    public async Task<Result<List<ReviewDto>>> Handle(GetAllReviewsByProductIdQuery request, CancellationToken cancellationToken)
    {
        var productsList = await reviewQuery.GetAllReviewsForProductAsync(request.productId, cancellationToken);

        if (productsList is null)
            return new Result<List<ReviewDto>>(false, $"Product with Id: {request.productId} doesn't exists.", null);

        return new Result<List<ReviewDto>>(true, null, productsList);
    }
}
