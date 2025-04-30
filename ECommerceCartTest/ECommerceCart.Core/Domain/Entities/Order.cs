using ECommerceCart.Core.Enums;

namespace ECommerceCart.Core.Domain.Entities;

public class Order : BaseModel
{
    public Guid OrderId { get; set; }
    public Guid UserId { get; set; }
    public Guid AddressId { get; set; }
    public string OrderStatus { get; set; } = OrderStatusOptions.Pending.ToString();

    public virtual ICollection<OrderVsProduct>? OrderVsProducts { get; set; }
}
