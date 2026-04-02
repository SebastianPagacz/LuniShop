using LuniShop.Application.Reviews.DTO;

namespace LuniShop.Application.Services;

public interface IReviewQueryService
{
    Task<ReviewDto> GetActiveItemByIdAsync(int id, CancellationToken cancellationToken);
    Task<List<ReviewDto>?> GetAllReviewsForProductAsync(int productId, CancellationToken cancellationToken);
}
