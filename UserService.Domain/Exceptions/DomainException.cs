namespace UserService.Domain.Exceptions;

public class DomainException : Exception
{
    public DomainException() : base() {}
    public DomainException(string message) : base(message) {}
    public DomainException(string message, Exception internalException) : base(message, internalException) {}
}