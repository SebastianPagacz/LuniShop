namespace LuniShop.Application.Reviews.DTO;

public record UpdateReviewRequest(string Title, string? Content, int Rating) { }
