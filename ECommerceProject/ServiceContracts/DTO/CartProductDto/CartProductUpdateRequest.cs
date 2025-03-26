using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.CartProductDto;

public class CartProductUpdateRequest
{
    [Required(ErrorMessage = "CartId is required")]
    public Guid CartId { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    public CartProduct ToUpdatedCartProduct(CartProduct cartProduct)
    {
        cartProduct.Quantity = Quantity;
        cartProduct.CartId = CartId;
        cartProduct.ProductId = ProductId;
        return cartProduct;
    }
}
