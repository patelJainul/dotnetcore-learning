namespace Entities;

public class Product
{
    public Guid ProductId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public string? ImageUrl { get; set; }

    public int Quantity { get; set; }

    public decimal SalePrice { get; set; }

    public decimal ActualPrice { get; set; }

    public Guid? CategoryId { get; set; }
}
