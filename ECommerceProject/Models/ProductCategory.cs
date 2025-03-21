namespace Entities;

public class ProductCategory
{
    public Guid CategoryId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
}
