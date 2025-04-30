namespace ECommerceCart.Core.Domain.Entities;

public class Product : BaseModel
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
}
