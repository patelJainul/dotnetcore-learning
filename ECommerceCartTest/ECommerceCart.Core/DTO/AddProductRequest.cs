using ECommerceCart.Core.Domain.Entities;

namespace ECommerceCart.Core.DTO;

public class AddProductRequest
{
    public required string Name { get; set; }
    public string? Description { get; set; }

    public Product ToProduct()
    {
        return new Product { Name = Name, Description = Description };
    }
}
