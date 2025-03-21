using Entities;
using ServiceContracts.DTO.ProductDto;

namespace ServiceContracts.DTO.CartProductDto;

public class CartProductResponse
{
    public Guid CartId { get; set; }

    public required ProductResponse Product { get; set; }

    public int Quantity { get; set; }
}

public static class CartProductExtensions
{
    public static CartProductResponse ToCartProductResponse(
        this CartProduct cartProduct,
        ProductResponse product
    )
    {
        return new CartProductResponse
        {
            CartId = cartProduct.CartId,
            Product = product,
            Quantity = cartProduct.Quantity,
        };
    }
}
