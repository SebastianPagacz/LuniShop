using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class DeleteReviewHandler(IRepository<Review> repository, IUnitOfWork uow, IProductQueryService queryService) : IRequestHandler<DeleteReviewCommand, Result<string>>
{
    public async Task<Result<string>> Handle(DeleteReviewCommand request, CancellationToken cancellationToken)
    {
        if (await queryService.GetActiveItemByIdAsync(request.ProductId, cancellationToken) is null)
            return new Result<string>(false, Message: $"Review with Id: {request.ReviewId} was not found.");

        var existingReview = await repository.GetByIdAsync(request.ReviewId, cancellationToken);

        if (existingReview.ProductId != request.ProductId)
            return new Result<string>(false, Message: $"Review with Id: {request.ReviewId} was not found.");

        if (existingReview is null || existingReview.IsDeleted)
            return new Result<string>(false, Message: $"Review with Id: {request.ReviewId} was not found.");

        existingReview.Delete();
        await uow.SaveAsync(cancellationToken);

        return new Result<string>(true, Value: $"Review with Id: {request.ReviewId} has been deleted.");
    }
}
