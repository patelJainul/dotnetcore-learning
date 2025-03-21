using Entities;

namespace ServiceContracts.DTO.ProductCategoryDto;

public class CategoryResponse
{
    public Guid CategoryId { get; set; }

    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }
}

public static class ProductCategoryResponseExtensions
{
    public static CategoryResponse ToCategoryResponse(this ProductCategory category)
    {
        return new CategoryResponse
        {
            CategoryId = category.CategoryId,
            Name = category.Name,
            Description = category.Description,
            ImageUrl = category.ImageUrl,
        };
    }
}
