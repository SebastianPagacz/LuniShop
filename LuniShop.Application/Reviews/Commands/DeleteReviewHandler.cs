using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class DeleteReviewHandler(IRepository<Product> repository, IUnitOfWork uow) : IRequestHandler<DeleteReviewCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.ProductId, cancellationToken);

        if (existingProduct is null || existingProduct.IsDeleted || !existingProduct.IsActive)
            return new Result<string>(false, Message: $"Review with Id: {request.ReviewId} was not found.");

        var existingReview = existingProduct.Reviews.FirstOrDefault(r => r.Id == request.ReviewId);

        if (existingReview is null || existingReview.IsDeleted || existingReview.ProductId != request.ProductId)
            return new Result<string>(false, Message: $"Review with Id: {request.ReviewId} was not found.");

        existingReview.Delete();
        await uow.SaveAsync(cancellationToken);

        return new Result<string>(true, Value: $"Review with Id: {request.ReviewId} has been deleted.");
    }
}
