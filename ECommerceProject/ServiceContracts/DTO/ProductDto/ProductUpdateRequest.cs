using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.ProductDto;

public class ProductUpdateRequest
{
    [Required(ErrorMessage = "ProductId is required")]
    public Guid ProductId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Price must be a positive number")]
    public decimal? Price { get; set; }

    public string? ImageUrl { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Quantity must be a positive integer")]
    public int? Quantity { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "SalePrice must be a positive number")]
    public decimal? SalePrice { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "ActualPrice must be a positive number")]
    public decimal? ActualPrice { get; set; }

    public Guid? CategoryId { get; set; }

    public Product ToUpdatedProduct(Product product)
    {
        product.Name = Name ?? product.Name;
        product.Description = Description ?? product.Description;
        product.Price = Price ?? product.Price;
        product.ImageUrl = ImageUrl ?? product.ImageUrl;
        product.Quantity = Quantity ?? product.Quantity;
        product.SalePrice = SalePrice ?? product.SalePrice;
        product.ActualPrice = ActualPrice ?? product.ActualPrice;
        product.CategoryId = CategoryId ?? product.CategoryId;
        return product;
    }
}
