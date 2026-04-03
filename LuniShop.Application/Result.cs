namespace LuniShop.Application;

public record Result<T>(bool IsSuccesfull, string? Message = default, T? Value = default) { }
