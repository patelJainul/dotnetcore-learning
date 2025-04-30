using ECommerceCart.Core.Domain.Entities;

namespace ECommerceCart.Core.DTO;

public class CartResponse
{
    public Guid CartId { get; set; }
    public Guid? UserId { get; set; }
    public virtual List<CartVsProductsResponse> CartVsProducts { get; set; } = [];
}

public static class CartResponseExtensions
{
    public static CartResponse ToCartResponse(this Cart cart)
    {
        return new CartResponse
        {
            CartId = cart.CartId,
            UserId = cart?.UserId,
            CartVsProducts =
                cart?.CartVsProducts?.Select(x => x.ToCartVsProductsResponse()).ToList() ?? [],
        };
    }
}
