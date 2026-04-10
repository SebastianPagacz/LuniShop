namespace UserService.Domain;

public interface IUnitOfWork
{
    Task SaveAsync(CancellationToken cancellationToken);
}
