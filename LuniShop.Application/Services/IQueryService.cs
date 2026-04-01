namespace LuniShop.Application.Services;

public interface IQueryService<T> where T : class
{
    Task<List<T>> GetAllActiveItemsAsync();
    Task<T> GetActiveItemByIdAsync(int id);
}
