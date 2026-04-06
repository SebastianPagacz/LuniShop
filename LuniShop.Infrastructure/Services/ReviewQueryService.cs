using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ReviewQueryService(AppDbContext context) : IReviewQueryService
{
    public async Task<ReviewDto?> GetActiveReviewByIdAsync(int productId, int reviewId, CancellationToken cancellationToken)
    {
        return await context.Products
            .AsNoTracking()
            .Where(p => p.IsActive && !p.IsDeleted && p.Id == productId)
            .SelectMany(p => p.Reviews)
            .Where(r => !r.IsDeleted && r.Id == reviewId)
            .Select(r => new ReviewDto(r.Id, r.Title, r.Content, r.Rating, r.ProductId))
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<List<ReviewDto>?> GetAllReviewsForProductAsync(int productId, CancellationToken cancellationToken)
    {
        var existingProduct = await context.Products
            .AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsActive && p.Id == productId)
            .FirstOrDefaultAsync(cancellationToken);

        if (existingProduct is null)
            return null;

        return await context.Reviews
            .AsNoTracking()
            .Where(r => r.ProductId == existingProduct.Id && !r.IsDeleted)
            .Select(r => new ReviewDto(r.Id, r.Title, r.Content, r.Rating, r.ProductId))
            .ToListAsync(cancellationToken);
    }
}
