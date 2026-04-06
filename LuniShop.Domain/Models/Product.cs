using LuniShop.Domain.Exceptions;

namespace LuniShop.Domain.Models;

public class Product
{
    public int Id { get; private set; }
    public string Name { get; private set; } = string.Empty;
    public string Description { get; private set; } = string.Empty;
    public decimal Price { get; private set; }
    public int Stock { get; private set; }
    public string Image { get; private set; } = string.Empty;
    public bool IsActive { get; private set; } = false;
    public bool IsDeleted { get; private set; } = false;
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
    public IReadOnlyCollection<Review> Reviews { get { return _reviews; } } // Readonly public prop
    private readonly List<Review> _reviews = new List<Review>(); // Private list that can be modified 
    public IReadOnlyCollection<ProductCategory> ProductCategories { get { return _productCategories; } }
    private readonly List<ProductCategory> _productCategories = new List<ProductCategory>();

    private Product() { } // Added for EF Core 

    private Product(string name, decimal price)
    {
        Name = name;
        Price = price;
    }

    public static Product Create(string name, decimal price)
    {
        if (string.IsNullOrEmpty(name)) 
            throw new DomainException("Product's name can't be empty.");
        
        if(price <= 0)
            throw new DomainException("Price can't be lower or equal to 0.");
        
        return new Product(name, price);
    }

    public void UpdateDetails(string name, string description)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Product's name can't be empty.");

        Name = name;
        Description = description;
        Update();
    }

    public void SetName(string name)
    {
        if (string.IsNullOrEmpty(name))
            throw new DomainException("Product's name can't be empty.");

        Name = name;
        Update();
    }

    public void SetDescription(string description)
    {
        Description = description;
        Update();
    }

    public void SetImage(string newImage)
    {
        if (string.IsNullOrEmpty(newImage))
            throw new DomainException("Image can't be empty."); // TODO: Think about the logic here

        Image = newImage;
        Update();
    }

    public void SetPrice(decimal newPrice)
    {
        if (newPrice <= 0) // Product's price can't be lower or equal to 0
            throw new DomainException("Price can't be lower or equal to 0.");

        Price = newPrice;
        Update();
    }

    public void SetStock(int newStock)
    {
        if (newStock < 0) // Stock can be equal to 0 but can't be lower
            throw new DomainException("Stock can't be negative.");

        Stock = newStock;
        Update();
    }

    public void AddStock(int quantity)
    {
        if (quantity <= 0)
            throw new DomainException("Stock can't be negative.");

        Stock += quantity;
    }

    public void SubtractStock(int quantity)
    {
        if (quantity <= 0 || quantity > Stock)
            throw new DomainException("Stock can't be negative.");

        Stock -= quantity;
    }

    public void Activate()
    {
        IsActive = true;
        Update();
    }

    public void Deactivate()
    {
        IsActive = false;
        Update();
    }

    public void Delete()
    {
        IsDeleted = true;
        Update();
    }

    private void Update()
    {
        UpdatedAt = DateTime.UtcNow;
    }

    public Review AddReview(string title, int rating, string? content)
    {
        var newReview = Review.CreateReview(Id, title, rating);

        if (!string.IsNullOrEmpty(content))
            newReview.SetContent(content);

        _reviews.Add(newReview);

        return newReview;
    }

    public void AssignCategory(int categoryId) // ToDo: Reconsider what should be returned (bool?)
    {
        if (_productCategories.Any(pc => pc.CategoryId == categoryId))
            throw new DomainException($"Category with Id {categoryId} is already assigned to the product.");

        var newProductCategory = ProductCategory.Create(this, categoryId);

        _productCategories.Add(newProductCategory);
    }
}
