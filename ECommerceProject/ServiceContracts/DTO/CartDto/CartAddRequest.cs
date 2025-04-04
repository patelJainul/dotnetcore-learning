using System.ComponentModel.DataAnnotations;

namespace ServiceContracts.DTO.CartDto;

public class CartAddRequest
{
    [Required(ErrorMessage = "User ID is required")]
    public Guid UserId { get; set; }

    [Required(ErrorMessage = "Product ID is required")]
    public Guid ProductId { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1")]
    public int Quantity { get; set; }
}
