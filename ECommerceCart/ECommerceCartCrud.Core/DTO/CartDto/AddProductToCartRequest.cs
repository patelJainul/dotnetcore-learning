using ECommerceCartCrud.Core.Domain.Entities;

namespace ECommerceCartCrud.Core.DTO.CartDto;

public class AddProductToCartRequest
{
    public Guid? CartId { get; set; }
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;

    public Cart ToCart()
    {
        var cartId = CartId ?? Guid.NewGuid();
        return new Cart()
        {
            CartId = cartId,
            UserId = UserId,
            CartProducts =
            [
                new CartVsProduct()
                {
                    CartId = cartId,
                    ProductId = ProductId,
                    Quantity = Quantity,
                },
            ],
        };
    }
}
