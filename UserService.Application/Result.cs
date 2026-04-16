namespace UserService.Application;

public record Result<T>(bool IsSuccesfull, T Value = default, string Message = default) {}
