using LuniShop.Domain.Exceptions;

namespace LuniShop.Domain.Models;

public class Review
{
	public int Id { get; private set; }
	public string Title { get; private set; } = string.Empty;
	public string Content { get; private set; } = string.Empty;
	public int Rating { get; private set; }
	public bool IsDeleted { get; private set; } = false;
	public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
	public DateTime UpdatedAt { get; private set; } = DateTime.UtcNow;
	// Relations
	public int ProductId { get; private set; }

	private Review() { } // EF Core constructor
	private Review(int productId, string title, int rating)
	{
		ProductId = productId;
		Title = title;
		Rating = rating;
	}

	internal static Review CreateReview(int productId, string title, int rating) // Minimal amount of properties requiered to create a business object. Internal so its accesible only in the same assembly/namespace
	{
		if (string.IsNullOrEmpty(title))
			throw new DomainException("Incorrect title - title can not be null or empty.");

		if (rating < 0 || rating > 5)
            throw new DomainException("Rating must be between 0 and 5.");

        return new Review(productId, title, rating);
	}

	public void SetTitle(string title)
	{
		if (string.IsNullOrEmpty(title))
			throw new DomainException("Incorrect title - title cann't be null or empty.");

		Title = title;
		Update();
	}
	public void SetContent(string content)
	{
        Content = content; // Content can be null therefore no validation required
        Update();
	}
	public void SetRating(int rating)
	{
		if (rating < 0 || rating > 5)
			throw new DomainException("Rating must be between 0 and 5.");
        
        Rating = rating;
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
}
