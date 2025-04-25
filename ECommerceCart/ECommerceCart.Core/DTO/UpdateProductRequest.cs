using ECommerceCart.Core.Domain.Entities;

namespace ECommerceCart.Core.DTO;

public class UpdateProductRequest
{
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Product ToProduct()
    {
        return new Product
        {
            ProductId = ProductId,
            Name = Name,
            Description = Description,
        };
    }
}
