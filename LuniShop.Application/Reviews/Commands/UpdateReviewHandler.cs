using LuniShop.Application.Reviews.DTO;
using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class UpdateReviewHandler(IUnitOfWork uow, IRepository<Product> repository) : IRequestHandler<UpdateReviewCommand, Result<ReviewDto>>
{
    public async Task<Result<ReviewDto>> Handle(UpdateReviewCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await repository.GetByIdAsync(request.ProductId, cancellationToken);
        
        if (existingProduct is null || existingProduct.IsDeleted || !existingProduct.IsActive)
            return new Result<ReviewDto>(false, Message: $"Review with Id: {request.Id} was not found.");

        var existinReview = existingProduct.Reviews.FirstOrDefault(r => r.Id == request.Id);

        if (existinReview is null || existinReview.IsDeleted || existinReview.ProductId != request.ProductId)
            return new Result<ReviewDto>(false, Message : $"Review with Id: {request.Id} was not found.");

        if (!string.IsNullOrWhiteSpace(request.Title))
            existinReview.SetTitle(request.Title);

        if(request.Content != null) // can be empty but shouldn't be null
            existinReview.SetContent(request.Content);

        if(request.Rating.HasValue)
            existinReview.SetRating((int)request.Rating);

        await uow.SaveAsync(cancellationToken);
        
        return new Result<ReviewDto>(true, Message : $"Review with Id: {existinReview.Id} has been updated.");
    }
}
