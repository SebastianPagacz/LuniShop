using LuniShop.Domain.Exceptions;
using System.Xml.Linq;

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
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;

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
    }

    public void SetDescription(string description)
    {
        Description = description;
    }

    public void SetImage(string newImage)
    {
        if (string.IsNullOrEmpty(newImage))
            throw new DomainException("Image can't be empty."); // TODO: Think about the logic here
        
        Image = newImage;
    }

    public void SetPrice(decimal newPrice)
    {
        if (newPrice <= 0) // Product's price can't be lower or equal to 0
            throw new DomainException("Price can't be lower or equal to 0.");

        Price = newPrice;
    }

    public void SetStock(int newStock)
    {
        if (newStock < 0) // Stock can be equal to 0 but can't be lower
            throw new DomainException("Stock can't be negative.");

        Stock = newStock;
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
            throw new DomainException("Price can't be negative.");

        Stock -= quantity;
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }
}
