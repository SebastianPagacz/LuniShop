namespace LuniShop.Domain.Exceptions;

public class InfrastructureException : Exception
{
    public InfrastructureException() : base() { }
    public InfrastructureException(string message) : base(message) { }
}
