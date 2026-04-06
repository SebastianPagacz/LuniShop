namespace LuniShop.Domain.Models;

public class ProductCategory // Explicit N:N realtionship 
{
    public Product Product { get; private set; }
    public int ProductId { get; private set; }
    public Category Category { get; private set; }
    public int CategoryId { get; private set; }

    private ProductCategory() { }
    private ProductCategory(Product product, int categoryId)
    {
        Product = product;
        ProductId = product.Id;
        CategoryId = categoryId;
    }

    internal static ProductCategory Create(Product product, int categoryId)
    {
        return new ProductCategory(product, categoryId);
    }
}
