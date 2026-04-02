namespace LuniShop.Application.Products.DTO;

public record ProductDto(int Id, string Name, string? Description, decimal Price, int Stock, string? Image){ }
