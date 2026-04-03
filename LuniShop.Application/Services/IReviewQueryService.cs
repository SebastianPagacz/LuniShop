using LuniShop.Application.Reviews.DTO;

namespace LuniShop.Application.Services;

public interface IReviewQueryService
{
    Task<ReviewDto?> GetActiveReviewByIdAsync(int productId, int reviewId, CancellationToken cancellationToken);
    Task<List<ReviewDto>?> GetAllReviewsForProductAsync(int productId, CancellationToken cancellationToken);
}
