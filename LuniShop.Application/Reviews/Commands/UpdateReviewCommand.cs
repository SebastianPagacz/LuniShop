using LuniShop.Application.Reviews.DTO;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public record UpdateReviewCommand(int Id, string Title, string? Content, int Rating, int ProductId) : IRequest<Result<ReviewDto>> { }
