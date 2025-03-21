using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.ProductCategoryDto;

public class CategoryAddRequest
{
    [Required(ErrorMessage = "Name is required")]
    public required string Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public ProductCategory ToProductCategory()
    {
        return new ProductCategory
        {
            Name = Name,
            Description = Description,
            ImageUrl = ImageUrl,
        };
    }
}
