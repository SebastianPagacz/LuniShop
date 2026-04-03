using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class AddReviewHandler(IUnitOfWork uow, IRepository<Product> productRepo) : IRequestHandler<AddReviewCommand, Result<string>>
{
    public async Task<Result<string>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await productRepo.GetByIdAsync(request.ProductId, cancellationToken);

        if (existingProduct is null || existingProduct.IsDeleted || !existingProduct.IsActive)
            return new Result<string>(false, $"Product with Id: {request.ProductId} was not found", null);
 
        var newReview = existingProduct.AddReview(request.Title, request.Rating, request.Content); // Adds new entry to private list, EF Core detects new untracked entry and inserts it into db

        await uow.SaveAsync();

        return new Result<string>(true, $"Review with Id: {newReview.Id} has been created", $"api/Products/{newReview.ProductId}/Reviews/{newReview.Id}");
    }
}
