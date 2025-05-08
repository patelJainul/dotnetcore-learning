namespace ECommerceCartCrud.Core.Domain.Entities;

public class Cart
{
    public Guid CartId { get; set; }
    public Guid? UserId { get; set; }
    public virtual ICollection<CartVsProduct>? CartProducts { get; set; }
}
