using Entities;
using ServiceContracts.DTO.CartProductDto;
using ServiceContracts.DTO.ProductDto;

namespace ServiceContracts.DTO.CartDto;

public class CartResponse
{
    public Guid CartId { get; set; }

    public Guid UserId { get; set; }

    public List<CartProductResponse> Products { get; set; } = [];

    public Cart ToCart()
    {
        return new Cart { CartId = CartId, UserId = UserId };
    }
}

public static class CartExtensions
{
    public static CartResponse ToCartResponse(this Cart cart, List<CartProductResponse>? products)
    {
        return new CartResponse
        {
            CartId = cart.CartId,
            UserId = cart.UserId,
            Products = [.. products ?? []],
        };
    }
}
