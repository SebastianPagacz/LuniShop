using LuniShop.Domain.Models;
using MediatR;

namespace LuniShop.Application.Reviews.Commands;

public record AddReviewCommand(int ProductId, string Title, string? Content, int Rating) : IRequest<Result<Review>> { } // ToDo: Think if the review is good result (maybe return id of newly created review)
