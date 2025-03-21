using Moq;
using ServiceContracts;
using ServiceContracts.DTO.ProductCategoryDto;
using ServiceContracts.DTO.ProductDto;
using Services;

namespace Tests;

public class ProductTests
{
    private readonly Mock<ICategoryServices> _mockCategoryServices;
    private readonly IProductServices _productServices;
    private readonly Guid _sampleCategoryId = Guid.NewGuid();

    public ProductTests()
    {
        _mockCategoryServices = new Mock<ICategoryServices>();
        _mockCategoryServices
            .Setup(m => m.GetCategoryById(It.IsAny<Guid?>()))
            .Returns(
                new CategoryResponse { CategoryId = _sampleCategoryId, Name = "Sample Category" }
            );

        _productServices = new ProductServices(_mockCategoryServices.Object);
    }

    [Fact]
    public void AddProduct_NullProduct_ThrowsArgumentNullException()
    {
        // Arrange
        ProductAddRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productServices.AddProduct(request));
    }

    [Fact]
    public void AddProduct_ValidProduct_ReturnsProductResponse()
    {
        // Arrange
        ProductAddRequest request = new()
        {
            Name = "Test Product",
            Price = 9.99m,
            ImageUrl = "test.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
            CategoryId = _sampleCategoryId,
        };

        // Act
        ProductResponse response = _productServices.AddProduct(request);
        ProductResponse? productResponse = _productServices.GetProductById(response.ProductId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.Name, response.Name);
        Assert.Equal(request.Price, response.Price);
        Assert.Equal(response.ProductId, productResponse.ProductId);
    }

    [Fact]
    public void AddProduct_MissingName_ThrowsArgumentException()
    {
        // Arrange
        ProductAddRequest request = new()
        {
            Name = "", // Missing name
            Price = 9.99m,
            ImageUrl = "test.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productServices.AddProduct(request));
    }

    [Fact]
    public void AddProduct_NegativePrice_ThrowsArgumentException()
    {
        // Arrange
        ProductAddRequest request = new()
        {
            Name = "Test Product",
            Price = -1.99m, // Negative price
            ImageUrl = "test.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _productServices.AddProduct(request));
    }

    [Fact]
    public void GetProductById_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? productId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productServices.GetProductById(productId));
    }

    [Fact]
    public void GetProductById_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid productId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _productServices.GetProductById(productId));
    }

    [Fact]
    public void GetAllProducts_NoProducts_ReturnsEmptyCollection()
    {
        // Act
        var products = _productServices.GetAllProducts();

        // Assert
        Assert.Empty(products);
    }

    [Fact]
    public void GetAllProducts_WithProducts_ReturnsAllProducts()
    {
        // Arrange
        ProductAddRequest request1 = new()
        {
            Name = "Test Product 1",
            Price = 9.99m,
            ImageUrl = "test1.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
            CategoryId = _sampleCategoryId,
        };
        ProductAddRequest request2 = new()
        {
            Name = "Test Product 2",
            Price = 19.99m,
            ImageUrl = "test2.jpg",
            Quantity = 20,
            SalePrice = 18.99m,
            ActualPrice = 19.99m,
            CategoryId = _sampleCategoryId,
        };
        _productServices.AddProduct(request1);
        _productServices.AddProduct(request2);

        // Act
        var products = _productServices.GetAllProducts();

        // Assert
        Assert.Equal(2, products.Count);
        Assert.Contains(products, p => p.Name == "Test Product 1");
        Assert.Contains(products, p => p.Name == "Test Product 2");
    }

    [Fact]
    public void UpdateProduct_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        ProductUpdateRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productServices.UpdateProduct(request));
    }

    [Fact]
    public void UpdateProduct_NonExistingProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        ProductUpdateRequest request = new()
        {
            ProductId = Guid.NewGuid(),
            Name = "Updated Product",
            Price = 29.99m,
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _productServices.UpdateProduct(request));
    }

    [Fact]
    public void UpdateProduct_ValidRequest_ReturnsUpdatedProduct()
    {
        // Arrange
        ProductAddRequest addRequest = new()
        {
            Name = "Test Product",
            Price = 9.99m,
            ImageUrl = "test.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
            CategoryId = _sampleCategoryId,
        };
        ProductResponse productResponse = _productServices.AddProduct(addRequest);

        ProductUpdateRequest updateRequest = new()
        {
            ProductId = productResponse.ProductId,
            Name = "Updated Product",
            Price = 19.99m,
            CategoryId = _sampleCategoryId,
        };

        // Act
        ProductResponse updatedResponse = _productServices.UpdateProduct(updateRequest);

        // Assert
        Assert.Equal(updateRequest.ProductId, updatedResponse.ProductId);
        Assert.Equal(updateRequest.Name, updatedResponse.Name);
        Assert.Equal(updateRequest.Price, updatedResponse.Price);
        Assert.Equal(_sampleCategoryId, updatedResponse?.Category?.CategoryId);
    }

    [Fact]
    public void DeleteProduct_NullId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? productId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _productServices.DeleteProduct(productId));
    }

    [Fact]
    public void DeleteProduct_NonExistingId_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid productId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _productServices.DeleteProduct(productId));
    }

    [Fact]
    public void DeleteProduct_ExistingId_ReturnsTrue()
    {
        // Arrange
        ProductAddRequest addRequest = new()
        {
            Name = "Test Product",
            Price = 9.99m,
            ImageUrl = "test.jpg",
            Quantity = 10,
            SalePrice = 8.99m,
            ActualPrice = 9.99m,
            CategoryId = _sampleCategoryId,
        };
        ProductResponse productResponse = _productServices.AddProduct(addRequest);

        // Act
        bool result = _productServices.DeleteProduct(productResponse.ProductId);

        // Assert
        Assert.True(result);
        Assert.Throws<KeyNotFoundException>(
            () => _productServices.GetProductById(productResponse.ProductId)
        );
    }
}
