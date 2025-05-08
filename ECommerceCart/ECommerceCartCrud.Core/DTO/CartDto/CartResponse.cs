using ECommerceCartCrud.Core.Domain.Entities;

namespace ECommerceCartCrud.Core.DTO.CartDto;

public class CartResponse
{
    public Guid CartId { get; set; }
    public Guid? UserId { get; set; }
    public virtual List<CartVsProductResponse>? CartProducts { get; set; }
}

public static class CartResponseExtensions
{
    public static CartResponse ToCartResponse(this Cart cart)
    {
        return new CartResponse()
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            CartProducts = cart.CartProducts?.Select(cp => cp.ToCartVsProductResponse()).ToList(),
        };
    }
}
