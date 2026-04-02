namespace LuniShop.Application.Reviews.DTO;

public record ReviewDto(int Id, string Title, string? Content, int Rating, int ProductId) { }
