namespace LuniShop.Application;

public record Result<T>(bool IsSuccesfull, string? Message, T? Value) { }
