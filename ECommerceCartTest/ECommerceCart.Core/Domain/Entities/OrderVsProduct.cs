namespace ECommerceCart.Core.Domain.Entities;

public class OrderVsProduct : BaseModel
{
    public Guid OrderVsProductId { get; set; }
    public required Guid OrderId { get; set; }
    public required Guid ProductId { get; set; }
    public required int Quantity { get; set; } = 1;
    public required decimal Price { get; set; } = 0.0m;
    public virtual Product? Product { get; set; }
}
