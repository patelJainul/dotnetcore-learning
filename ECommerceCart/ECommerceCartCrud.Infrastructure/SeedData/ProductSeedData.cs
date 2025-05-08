using ECommerceCartCrud.Core.Domain.Entities;

namespace ECommerceCartCrud.Infrastructure.SeedData;

public class ProductSeedData
{
    public static List<Product> Seed()
    {
        return
        [
            new Product()
            {
                ProductId = Guid.Parse("72a86086-6243-48fd-991c-41f5ec37efd3"),
                Name = "Product 1",
                Description = "Description for Product 1",
                Price = 10.99d,
            },
            new Product()
            {
                ProductId = Guid.Parse("ee72e4e3-1096-4917-bde5-880d25da637d"),
                Name = "Product 2",
                Description = "Description for Product 2",
                Price = 12.67d,
            },
        ];
    }
}
