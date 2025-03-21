using ServiceContracts;
using ServiceContracts.DTO.ProductCategoryDto;
using Services;

namespace Tests;

public class CategoryTests
{
    private readonly ICategoryServices _categoryServices = new CategoryServices();

    [Fact]
    public void AddCategory_NullCategory_ThrowsArgumentNullException()
    {
        // Arrange
        CategoryAddRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryServices.AddCategory(request));
    }

    [Fact]
    public void AddCategory_ValidCategory_ReturnsCategoryResponse()
    {
        // Arrange
        CategoryAddRequest request = new()
        {
            Name = "Electronics",
            Description = "Electronic items",
            ImageUrl = "electronics.jpg",
        };

        // Act
        CategoryResponse response = _categoryServices.AddCategory(request);
        CategoryResponse? categoryResponse = _categoryServices.GetCategoryById(response.CategoryId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Description, response.Description);
        Assert.Equal(response.CategoryId, categoryResponse.CategoryId);
    }

    [Fact]
    public void AddCategory_MissingName_ThrowsArgumentException()
    {
        // Arrange
        CategoryAddRequest request = new()
        {
            Name = "", // Missing name
            Description = "Electronic items",
            ImageUrl = "electronics.jpg",
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _categoryServices.AddCategory(request));
    }

    [Fact]
    public void GetCategoryById_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? categoryId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryServices.GetCategoryById(categoryId));
    }

    [Fact]
    public void GetCategoryById_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid categoryId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _categoryServices.GetCategoryById(categoryId));
    }

    [Fact]
    public void GetAllCategories_NoCategories_ReturnsEmptyCollection()
    {
        // Act
        var categories = _categoryServices.GetAllCategories();

        // Assert
        Assert.Empty(categories);
    }

    [Fact]
    public void GetAllCategories_WithCategories_ReturnsAllCategories()
    {
        // Arrange
        CategoryAddRequest request1 = new()
        {
            Name = "Electronics",
            Description = "Electronic items",
            ImageUrl = "electronics.jpg",
        };
        CategoryAddRequest request2 = new()
        {
            Name = "Clothing",
            Description = "Clothing items",
            ImageUrl = "clothing.jpg",
        };
        _categoryServices.AddCategory(request1);
        _categoryServices.AddCategory(request2);

        // Act
        var categories = _categoryServices.GetAllCategories();

        // Assert
        Assert.Equal(2, categories.Count);
        Assert.Contains(categories, c => c.Name == "Electronics");
        Assert.Contains(categories, c => c.Name == "Clothing");
    }

    [Fact]
    public void UpdateCategory_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CategoryUpdateRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryServices.UpdateCategory(request));
    }

    [Fact]
    public void UpdateCategory_NonExistingCategory_ThrowsKeyNotFoundException()
    {
        // Arrange
        CategoryUpdateRequest request = new()
        {
            CategoryId = Guid.NewGuid(),
            Name = "Updated Category",
            Description = "Updated description",
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _categoryServices.UpdateCategory(request));
    }

    [Fact]
    public void UpdateCategory_ValidRequest_ReturnsUpdatedCategory()
    {
        // Arrange
        CategoryAddRequest addRequest = new()
        {
            Name = "Electronics",
            Description = "Electronic items",
            ImageUrl = "electronics.jpg",
        };
        CategoryResponse categoryResponse = _categoryServices.AddCategory(addRequest);

        CategoryUpdateRequest updateRequest = new()
        {
            CategoryId = categoryResponse.CategoryId,
            Name = "Updated Electronics",
            Description = "Updated electronic items",
            ImageUrl = "updated-electronics.jpg",
        };

        // Act
        CategoryResponse updatedResponse = _categoryServices.UpdateCategory(updateRequest);

        // Assert
        Assert.Equal(updateRequest.CategoryId, updatedResponse.CategoryId);
        Assert.Equal(updateRequest.Name, updatedResponse.Name);
        Assert.Equal(updateRequest.Description, updatedResponse.Description);
        Assert.Equal(updateRequest.ImageUrl, updatedResponse.ImageUrl);
    }

    [Fact]
    public void DeleteCategory_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? categoryId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _categoryServices.DeleteCategory(categoryId));
    }

    [Fact]
    public void DeleteCategory_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid categoryId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _categoryServices.DeleteCategory(categoryId));
    }

    [Fact]
    public void DeleteCategory_ExistingId_ReturnsTrue()
    {
        // Arrange
        CategoryAddRequest addRequest = new()
        {
            Name = "Electronics",
            Description = "Electronic items",
            ImageUrl = "electronics.jpg",
        };
        CategoryResponse categoryResponse = _categoryServices.AddCategory(addRequest);

        // Act
        bool result = _categoryServices.DeleteCategory(categoryResponse.CategoryId);

        // Assert
        Assert.True(result);
        Assert.Throws<KeyNotFoundException>(
            () => _categoryServices.GetCategoryById(categoryResponse.CategoryId)
        );
    }
}
