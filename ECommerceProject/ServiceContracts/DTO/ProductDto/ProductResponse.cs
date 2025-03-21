using Entities;
using ServiceContracts.DTO.ProductCategoryDto;

namespace ServiceContracts.DTO.ProductDto;

public class ProductResponse
{
    public Guid ProductId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public int Quantity { get; set; }

    public decimal SalePrice { get; set; }

    public decimal ActualPrice { get; set; }

    public CategoryResponse? Category { get; set; }
}

public static class ProductExtensions
{
    public static ProductResponse ToProductResponse(
        this Product product,
        CategoryResponse? category
    )
    {
        return new ProductResponse
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
            ImageUrl = product.ImageUrl,
            Quantity = product.Quantity,
            SalePrice = product.SalePrice,
            ActualPrice = product.ActualPrice,
            Category = category,
        };
    }
}
