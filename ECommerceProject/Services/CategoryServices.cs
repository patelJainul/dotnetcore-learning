using Entities;
using ServiceContracts;
using ServiceContracts.DTO.ProductCategoryDto;
using Services.Helpers;

namespace Services;

public class CategoryServices : ICategoryServices
{
    private readonly List<ProductCategory> _categories = [];

    public CategoryResponse AddCategory(CategoryAddRequest? categoryAddRequest)
    {
        ArgumentNullException.ThrowIfNull(categoryAddRequest);
        ValidationHelper.ModelValidation(categoryAddRequest);

        var category = categoryAddRequest.ToProductCategory();
        _categories.Add(category);

        return category.ToCategoryResponse();
    }

    public List<CategoryResponse> GetAllCategories()
    {
        return [.. _categories.Select(category => category.ToCategoryResponse())];
    }

    public CategoryResponse GetCategoryById(Guid? categoryId)
    {
        ArgumentNullException.ThrowIfNull(categoryId);

        var category =
            _categories.FirstOrDefault(temp => temp.CategoryId == categoryId)
            ?? throw new KeyNotFoundException($"Category with id {categoryId} not found");

        return category.ToCategoryResponse();
    }

    public CategoryResponse UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(categoryUpdateRequest);
        ValidationHelper.ModelValidation(categoryUpdateRequest);

        var category =
            _categories.FirstOrDefault(temp => temp.CategoryId == categoryUpdateRequest.CategoryId)
            ?? throw new KeyNotFoundException(
                $"Category with id {categoryUpdateRequest.CategoryId} not found"
            );

        var updatedCategory = categoryUpdateRequest.ToUpdatedProductCategory(category);
        _categories.Remove(category);
        _categories.Add(updatedCategory);

        return updatedCategory.ToCategoryResponse();
    }

    public bool DeleteCategory(Guid? categoryId)
    {
        ArgumentNullException.ThrowIfNull(categoryId);

        var category =
            _categories.FirstOrDefault(temp => temp.CategoryId == categoryId)
            ?? throw new KeyNotFoundException($"Category with id {categoryId} not found");
        var isRemoved = _categories.Remove(category);

        if (!isRemoved)
        {
            throw new Exception("Failed to remove category");
        }
        return isRemoved;
    }
}
