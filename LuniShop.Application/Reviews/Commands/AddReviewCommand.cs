using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public record AddReviewCommand(int ProductId, string Title, string? Content, int Rating) : IRequest<Result<string>> { }
