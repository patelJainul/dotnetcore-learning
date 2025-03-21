using ServiceContracts.DTO.ProductCategoryDto;

namespace ServiceContracts;

public interface ICategoryServices
{
    public CategoryResponse AddCategory(CategoryAddRequest? categoryAddRequest);

    public List<CategoryResponse> GetAllCategories();

    public CategoryResponse GetCategoryById(Guid? categoryId);

    public CategoryResponse UpdateCategory(CategoryUpdateRequest? categoryUpdateRequest);

    public bool DeleteCategory(Guid? categoryId);
}
