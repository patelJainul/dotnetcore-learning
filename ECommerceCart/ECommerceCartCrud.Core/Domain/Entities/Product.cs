namespace ECommerceCartCrud.Core.Domain.Entities;

public class Product
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}
