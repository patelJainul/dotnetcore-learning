using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.ProductDto;

public class ProductAddRequest
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    public string? Description { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal Price { get; set; }

    [Required(ErrorMessage = "ImageUrl is required")]
    public string? ImageUrl { get; set; }

    [Required(ErrorMessage = "Quantity is required")]
    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
    public int Quantity { get; set; }

    [Required(ErrorMessage = "SalePrice is required")]
    public decimal SalePrice { get; set; }

    [Required(ErrorMessage = "ActualPrice is required")]
    public decimal ActualPrice { get; set; }

    public Guid? CategoryId { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            Name = Name,
            Description = Description,
            Price = Price,
            ImageUrl = ImageUrl,
            Quantity = Quantity,
            SalePrice = SalePrice,
            ActualPrice = ActualPrice,
            CategoryId = CategoryId,
        };
    }
}
