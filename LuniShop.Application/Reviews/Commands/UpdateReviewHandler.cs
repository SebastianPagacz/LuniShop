using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class UpdateReviewHandler(IUnitOfWork uow, IRepository<Review> repository, IProductQueryService queryService) : IRequestHandler<UpdateReviewCommand, Result<ReviewDto>>
{
    public async Task<Result<ReviewDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        if (await queryService.GetActiveItemByIdAsync(request.ProductId, cancellationToken) is null)
            return new Result<ReviewDto>(false, Message: $"Review with Id: {request.Id} was not found."); // I do not give information that the product doesn't exist rather that review doesn't exists for particular product
        
        var existinReview = await repository.GetByIdAsync(request.Id, cancellationToken);

        if (existinReview.ProductId != request.ProductId)
            return new Result<ReviewDto>(false, Message: $"Review with Id: {request.Id} was not found.");

        if (existinReview is null || existinReview.IsDeleted)
            return new Result<ReviewDto>(false, Message : $"Review with Id: {request.Id} was not found.");

        if (!string.IsNullOrEmpty(request.Title))
            existinReview.SetTitle(request.Title);

        if(request.Content != null) // can be empty but shouldn't be null
            existinReview.SetContent(request.Content);

        existinReview.SetRating(request.Rating);

        await uow.SaveAsync(cancellationToken);
        
        return new Result<ReviewDto>(true, Message : $"Review with Id: {existinReview.Id} has been updated.");
    }
}
