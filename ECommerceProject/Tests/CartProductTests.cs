using Entities;
using Moq;
using ServiceContracts;
using ServiceContracts.DTO.CartProductDto;
using ServiceContracts.DTO.ProductDto;
using Services;

namespace Tests;

public class CartProductTests
{
    private readonly Mock<IProductServices> _mockProductServices;
    private readonly ICartProductServices _cartProductServices;
    private readonly Guid _sampleCartId = Guid.NewGuid();
    private readonly Guid _sampleProductId = Guid.NewGuid();
    private readonly ProductResponse _sampleProductResponse;

    public CartProductTests()
    {
        _mockProductServices = new Mock<IProductServices>();
        _sampleProductResponse = new ProductResponse
        {
            ProductId = _sampleProductId,
            Name = "Sample Product",
            Price = 9.99m,
            ImageUrl = "sample.jpg",
            Category = new ServiceContracts.DTO.ProductCategoryDto.CategoryResponse
            {
                CategoryId = Guid.NewGuid(),
                Name = "Sample Category",
            },
        };

        _mockProductServices
            .Setup(m => m.GetProductById(_sampleProductId))
            .Returns(_sampleProductResponse);

        _cartProductServices = new CartProductServices(_mockProductServices.Object);
    }

    [Fact]
    public void AddCartProduct_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CartProductAddRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartProductServices.AddCartProduct(request));
    }

    [Fact]
    public void AddCartProduct_ValidRequest_ReturnsCartProductResponse()
    {
        // Arrange
        CartProductAddRequest request = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 2,
        };

        // Act
        CartProductResponse response = _cartProductServices.AddCartProduct(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.CartId, response.CartId);
        Assert.Equal(request.ProductId, response.Product.ProductId);
        Assert.Equal(request.Quantity, response.Quantity);
    }

    [Fact]
    public void AddCartProduct_InvalidQuantity_ThrowsArgumentException()
    {
        // Arrange
        CartProductAddRequest request = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 0, // Invalid quantity
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartProductServices.AddCartProduct(request));
    }

    [Fact]
    public void AddCartProduct_MissingCartId_ThrowsArgumentException()
    {
        // Arrange
        CartProductAddRequest request = new()
        {
            CartId = Guid.Empty,
            ProductId = _sampleProductId,
            Quantity = 2,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartProductServices.AddCartProduct(request));
    }

    [Fact]
    public void AddCartProduct_MissingProductId_ThrowsArgumentException()
    {
        // Arrange
        CartProductAddRequest request = new()
        {
            CartId = _sampleCartId,
            ProductId = Guid.Empty,
            Quantity = 2,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartProductServices.AddCartProduct(request));
    }

    [Fact]
    public void GetAllCartProducts_NullCartId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? cartId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartProductServices.GetAllCartProducts(cartId));
    }

    [Fact]
    public void GetAllCartProducts_EmptyCart_ReturnsEmptyCollection()
    {
        // Act
        var products = _cartProductServices.GetAllCartProducts(Guid.NewGuid());

        // Assert
        Assert.Empty(products);
    }

    [Fact]
    public void GetAllCartProducts_WithProducts_ReturnsAllCartProducts()
    {
        // Arrange
        CartProductAddRequest request1 = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 2,
        };
        _cartProductServices.AddCartProduct(request1);

        // Act
        var products = _cartProductServices.GetAllCartProducts(_sampleCartId);

        // Assert
        Assert.Single(products);
        Assert.Equal(_sampleCartId, products[0].CartId);
        Assert.Equal(_sampleProductId, products[0].Product.ProductId);
    }

    [Fact]
    public void GetCartProductById_NullCartId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? cartId = null;
        Guid? productId = _sampleProductId;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => _cartProductServices.GetCartProductById(cartId, productId)
        );
    }

    [Fact]
    public void GetCartProductById_NullProductId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? cartId = _sampleCartId;
        Guid? productId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => _cartProductServices.GetCartProductById(cartId, productId)
        );
    }

    [Fact]
    public void GetCartProductById_NonExistingIds_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid cartId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(
            () => _cartProductServices.GetCartProductById(cartId, productId)
        );
    }

    [Fact]
    public void GetCartProductById_ExistingIds_ReturnsCartProductResponse()
    {
        // Arrange
        CartProductAddRequest request = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 2,
        };
        _cartProductServices.AddCartProduct(request);

        // Act
        var response = _cartProductServices.GetCartProductById(_sampleCartId, _sampleProductId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(_sampleCartId, response.CartId);
        Assert.Equal(_sampleProductId, response.Product.ProductId);
        Assert.Equal(2, response.Quantity);
    }

    [Fact]
    public void UpdateCartProduct_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CartProductUpdateRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartProductServices.UpdateCartProduct(request));
    }

    [Fact]
    public void UpdateCartProduct_NonExistingProduct_ThrowsKeyNotFoundException()
    {
        // Arrange
        CartProductUpdateRequest request = new()
        {
            CartId = Guid.NewGuid(),
            ProductId = Guid.NewGuid(),
            Quantity = 3,
        };

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _cartProductServices.UpdateCartProduct(request));
    }

    [Fact]
    public void UpdateCartProduct_ValidRequest_ReturnsUpdatedCartProductResponse()
    {
        // Arrange
        CartProductAddRequest addRequest = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 2,
        };
        _cartProductServices.AddCartProduct(addRequest);

        CartProductUpdateRequest updateRequest = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 5,
        };

        // Act
        var response = _cartProductServices.UpdateCartProduct(updateRequest);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(_sampleCartId, response.CartId);
        Assert.Equal(_sampleProductId, response.Product.ProductId);
        Assert.Equal(5, response.Quantity);
    }

    [Fact]
    public void UpdateCartProduct_EmptyCartId_ThrowsArgumentException()
    {
        // Arrange
        CartProductUpdateRequest request = new()
        {
            CartId = Guid.Empty,
            ProductId = _sampleProductId,
            Quantity = 3,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartProductServices.UpdateCartProduct(request));
    }

    [Fact]
    public void UpdateCartProduct_EmptyProductId_ThrowsArgumentException()
    {
        // Arrange
        CartProductUpdateRequest request = new()
        {
            CartId = _sampleCartId,
            ProductId = Guid.Empty,
            Quantity = 3,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartProductServices.UpdateCartProduct(request));
    }

    [Fact]
    public void DeleteCartProduct_NullCartId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? cartId = null;
        Guid? productId = _sampleProductId;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => _cartProductServices.DeleteCartProduct(cartId, productId)
        );
    }

    [Fact]
    public void DeleteCartProduct_NullProductId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? cartId = _sampleCartId;
        Guid? productId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(
            () => _cartProductServices.DeleteCartProduct(cartId, productId)
        );
    }

    [Fact]
    public void DeleteCartProduct_NonExistingIds_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid cartId = Guid.NewGuid();
        Guid productId = Guid.NewGuid();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(
            () => _cartProductServices.DeleteCartProduct(cartId, productId)
        );
    }

    [Fact]
    public void DeleteCartProduct_ExistingIds_ReturnsTrue()
    {
        // Arrange
        CartProductAddRequest addRequest = new()
        {
            CartId = _sampleCartId,
            ProductId = _sampleProductId,
            Quantity = 2,
        };
        _cartProductServices.AddCartProduct(addRequest);

        // Act
        bool result = _cartProductServices.DeleteCartProduct(_sampleCartId, _sampleProductId);

        // Assert
        Assert.True(result);
        Assert.Throws<KeyNotFoundException>(
            () => _cartProductServices.GetCartProductById(_sampleCartId, _sampleProductId)
        );
    }
}
