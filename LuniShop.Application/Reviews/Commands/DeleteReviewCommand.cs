using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public record DeleteReviewCommand(int ProductId, int ReviewId) : IRequest<Result<string>> { }