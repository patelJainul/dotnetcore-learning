using ECommerceCart.Core.Domain.Entities;

namespace ECommerceCart.Core.DTO;

public class AddCartRequest
{
    public Guid? UserId { get; set; }
    public Guid? CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;

    public Cart ToCart()
    {
        var id = CartId ?? Guid.NewGuid();
        return new Cart
        {
            UserId = UserId,
            CartId = id,
            CartVsProducts =
            [
                new()
                {
                    ProductId = ProductId,
                    Quantity = Quantity,
                    CartId = id,
                },
            ],
        };
    }
}
