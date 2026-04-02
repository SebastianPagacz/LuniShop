using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using LuniShop.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace LuniShop.Infrastructure.Services;

public class ReviewQueryService(AppDbContext context) : IReviewQueryService
{
    public async Task<ReviewDto> GetActiveItemByIdAsync(int id)
    {
        return await context.Reviews.AsNoTracking()
            .Where(r => !r.IsDeleted)
            .Select(r => new ReviewDto(r.Id, r.Title, r.Content, r.Rating, r.ProductId))
            .FirstOrDefaultAsync(r => r.Id == id);
    }

    public async Task<List<ReviewDto>?> GetAllReviewsForProductAsync(int productId)
    {
        var existingProduct = await context.Products
            .AsNoTracking()
            .Where(p => !p.IsDeleted && p.IsActive)
            .FirstOrDefaultAsync(p => p.Id == productId); // Think about handlign not exisitng products, I might do it in app layer. If product does not exists it will throw an exception

        if (existingProduct is null)
            return null;

        return await context.Reviews
            .AsNoTracking()
            .Where(r => r.ProductId == existingProduct.Id && !r.IsDeleted)
            .Select(r => new ReviewDto(r.Id, r.Title, r.Content, r.Rating, r.ProductId))
            .ToListAsync();
    }
}
