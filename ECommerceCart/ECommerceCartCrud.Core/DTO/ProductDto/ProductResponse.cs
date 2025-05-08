using ECommerceCartCrud.Core.Domain.Entities;

namespace ECommerceCartCrud.Core.DTO.ProductDto;

public class ProductResponse
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
}

public static class ProductResponseExtensions
{
    public static ProductResponse ToProductResponse(this Product product)
    {
        return new ProductResponse()
        {
            ProductId = product.ProductId,
            Name = product.Name,
            Description = product.Description,
            Price = product.Price,
        };
    }
}
