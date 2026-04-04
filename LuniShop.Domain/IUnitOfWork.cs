namespace LuniShop.Domain;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}
