namespace ECommerceCart.Core.Domain.Entities;

public class CartVsProducts : BaseModel
{
    public Guid CartVsProductsId { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public required int Quantity { get; set; } = 1;
    public virtual Product? Product { get; set; }
}
