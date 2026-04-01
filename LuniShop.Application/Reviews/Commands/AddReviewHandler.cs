using LuniShop.Application.Products.DTO;
using LuniShop.Application.Services;
using LuniShop.Domain;
using LuniShop.Domain.Models;
using LuniShop.Domain.Repository;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public class AddReviewHandler(IUnitOfWork uow, IRepository<Review> repository, IQueryService<ProductDto> queryService) : IRequestHandler<AddReviewCommand, Result<Review>>
{
    public async Task<Result<Review>> Handle(AddReviewCommand request, CancellationToken cancellationToken)
    {
        if (queryService.GetActiveItemByIdAsync(request.ProductId) is null)
            return new Result<Review>(false, $"Product with Id: {request.ProductId} was not found", null);
        
        var newReview = 
    }
}
