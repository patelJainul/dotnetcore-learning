using System;
using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.CartProductDto;

public class CartProductAddRequest
{
    [Required(ErrorMessage = "CartId is required")]
    [RegularExpression(
        @"^({?([0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12})}?)$",
        ErrorMessage = "Invalid GUID format."
    )]
    public Guid CartId { get; set; }

    [Required(ErrorMessage = "ProductId is required")]
    [RegularExpression(
        @"^({?([0-9A-Fa-f]{8}-([0-9A-Fa-f]{4}-){3}[0-9A-Fa-f]{12})}?)$",
        ErrorMessage = "Invalid GUID format."
    )]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }

    public CartProduct ToCartProduct()
    {
        return new CartProduct
        {
            CartId = CartId,
            ProductId = ProductId,
            Quantity = Quantity,
        };
    }
}
