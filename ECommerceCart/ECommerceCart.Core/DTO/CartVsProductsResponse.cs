using ECommerceCart.Core.Domain.Entities;

namespace ECommerceCart.Core.DTO;

public class CartVsProductsResponse
{
    public Guid CartVsProductsId { get; set; }
    public required int Quantity { get; set; } = 1;
    public virtual ProductResponse? Product { get; set; } = null!;
}

public static class CartVsProductsResponseExtensions
{
    public static CartVsProductsResponse ToCartVsProductsResponse(
        this CartVsProducts cartVsProducts
    )
    {
        return new CartVsProductsResponse
        {
            CartVsProductsId = cartVsProducts.CartVsProductsId,
            Quantity = cartVsProducts.Quantity,
            Product = cartVsProducts?.Product?.ToProductResponse(),
        };
    }
}
