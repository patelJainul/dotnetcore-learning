using System.ComponentModel.DataAnnotations;
using Entities;

namespace ServiceContracts.DTO.ProductCategoryDto;

public class CategoryUpdateRequest
{
    [Required(ErrorMessage = "CategoryId is required")]
    public Guid CategoryId { get; set; }

    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public ProductCategory ToUpdatedProductCategory(ProductCategory productCategory)
    {
        productCategory.Name = Name ?? productCategory.Name;
        productCategory.Description = Description ?? productCategory.Description;
        productCategory.ImageUrl = ImageUrl ?? productCategory.ImageUrl;
        return productCategory;
    }
}
