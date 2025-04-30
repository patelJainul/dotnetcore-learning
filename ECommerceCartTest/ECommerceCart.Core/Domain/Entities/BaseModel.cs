namespace ECommerceCart.Core.Domain.Entities;

public class BaseModel
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}
