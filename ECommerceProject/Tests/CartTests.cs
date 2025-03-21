using Moq;
using ServiceContracts;
using ServiceContracts.DTO.CartDto;
using ServiceContracts.DTO.CartProductDto;
using ServiceContracts.DTO.ProductDto;
using Services;

namespace Tests;

public class CartTests
{
    private readonly Mock<ICartProductServices> _mockCartProductServices;
    private readonly Mock<IProductServices> _mockProductServices;
    private readonly ICartServices _cartServices;
    private readonly Guid _sampleCartId = Guid.NewGuid();
    private readonly Guid _sampleUserId = Guid.NewGuid();
    private readonly Guid _sampleProductId = Guid.NewGuid();

    public CartTests()
    {
        // Set up mocks
        _mockProductServices = new Mock<IProductServices>();
        _mockCartProductServices = new Mock<ICartProductServices>();

        // Set up mock for product info in ProductServices
        _mockProductServices
            .Setup(m => m.GetProductById(_sampleProductId))
            .Returns(
                new ProductResponse
                {
                    ProductId = _sampleProductId,
                    Name = "Sample Product",
                    Price = 9.99m,
                    ImageUrl = "sample.jpg",
                }
            );

        // Setup for GetAllCartProducts
        _mockCartProductServices
            .Setup(m => m.GetAllCartProducts(It.IsAny<Guid>()))
            .Returns(
                [
                    new CartProductResponse
                    {
                        CartId = _sampleCartId,
                        Product = new ProductResponse
                        {
                            ProductId = _sampleProductId,
                            Name = "Sample Product",
                            Price = 9.99m,
                            ImageUrl = "sample.jpg",
                        },
                        Quantity = 2,
                    },
                ]
            );

        // Setup for GetCartProductById
        _mockCartProductServices
            .Setup(m => m.GetCartProductById(_sampleCartId, _sampleProductId))
            .Returns(
                new CartProductResponse
                {
                    CartId = _sampleCartId,
                    Product = new ProductResponse
                    {
                        ProductId = _sampleProductId,
                        Name = "Sample Product",
                        Price = 9.99m,
                        ImageUrl = "sample.jpg",
                    },
                    Quantity = 2,
                }
            );

        // Setup for GetCartProductById (throw KeyNotFoundException if product doesn't exist)
        _mockCartProductServices
            .Setup(m =>
                m.GetCartProductById(It.IsAny<Guid?>(), It.Is<Guid?>(id => id != _sampleProductId))
            )
            .Throws<KeyNotFoundException>();

        // Setup for AddCartProduct
        _mockCartProductServices
            .Setup(m => m.AddCartProduct(It.IsAny<CartProductAddRequest>()))
            .Returns(
                (CartProductAddRequest request) =>
                    new CartProductResponse
                    {
                        CartId = request.CartId,
                        Product = new ProductResponse
                        {
                            ProductId = request.ProductId,
                            Name = "Sample Product",
                            Price = 9.99m,
                            ImageUrl = "sample.jpg",
                        },
                        Quantity = request.Quantity,
                    }
            );

        // Setup for UpdateCartProduct
        _mockCartProductServices
            .Setup(m => m.UpdateCartProduct(It.IsAny<CartProductUpdateRequest>()))
            .Returns(
                (CartProductUpdateRequest request) =>
                    new CartProductResponse
                    {
                        CartId = request.CartId,
                        Product = new ProductResponse
                        {
                            ProductId = request.ProductId,
                            Name = "Sample Product",
                            Price = 9.99m,
                            ImageUrl = "sample.jpg",
                        },
                        Quantity = request.Quantity,
                    }
            );

        // Setup for DeleteCartProduct
        _mockCartProductServices
            .Setup(m => m.DeleteCartProduct(It.IsAny<Guid?>(), _sampleProductId))
            .Returns(true);

        // Create CartServices with the ICartProductServices mock
        _cartServices = new CartServices(_mockCartProductServices.Object);
    }

    [Fact]
    public void AddToCart_NullRequest_ThrowsArgumentNullException()
    {
        // Arrange
        CartAddRequest? request = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartServices.AddToCart(request));
    }

    [Fact]
    public void AddToCart_MissingUserId_ThrowsArgumentException()
    {
        // Arrange
        CartAddRequest request = new()
        {
            UserId = Guid.Empty,
            ProductId = _sampleProductId,
            Quantity = 2,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartServices.AddToCart(request));
    }

    [Fact]
    public void AddToCart_MissingProductId_ThrowsArgumentException()
    {
        // Arrange
        CartAddRequest request = new()
        {
            UserId = _sampleUserId,
            ProductId = Guid.Empty,
            Quantity = 2,
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartServices.AddToCart(request));
    }

    [Fact]
    public void AddToCart_InvalidQuantity_ThrowsArgumentException()
    {
        // Arrange
        CartAddRequest request = new()
        {
            UserId = _sampleUserId,
            ProductId = _sampleProductId,
            Quantity = 0, // Invalid quantity
        };

        // Act & Assert
        Assert.Throws<ArgumentException>(() => _cartServices.AddToCart(request));
    }

    [Fact]
    public void AddToCart_NewCart_CreatesCartAndReturnsResponse()
    {
        // Arrange
        CartAddRequest request = new()
        {
            UserId = Guid.NewGuid(), // New user ID for new cart
            ProductId = _sampleProductId,
            Quantity = 2,
        };

        // Act
        var response = _cartServices.AddToCart(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(request.UserId, response.UserId);
        Assert.Contains(response.Products, p => p.Product.ProductId == _sampleProductId);
    }

    [Fact]
    public void AddToCart_ExistingCart_UpdatesCartAndReturnsResponse()
    {
        // Arrange
        // First, add a product to create a cart
        CartAddRequest initialRequest = new()
        {
            UserId = _sampleUserId,
            ProductId = _sampleProductId,
            Quantity = 1,
        };
        _cartServices.AddToCart(initialRequest);

        // Now add another product or update existing one
        CartAddRequest request = new()
        {
            UserId = _sampleUserId,
            ProductId = _sampleProductId,
            Quantity = 3,
        };

        // Act
        var response = _cartServices.AddToCart(request);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(_sampleUserId, response.UserId);
        Assert.Contains(response.Products, p => p.Product.ProductId == _sampleProductId);
    }

    [Fact]
    public void GetCart_NullUserId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? userId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartServices.GetCart(userId));
    }

    [Fact]
    public void GetCart_NonExistingCart_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid nonExistingUserId = Guid.NewGuid();

        // Override the default mock setup for this specific test
        _mockCartProductServices
            .Setup(m => m.GetAllCartProducts(It.IsAny<Guid>()))
            .Throws<KeyNotFoundException>();

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _cartServices.GetCart(nonExistingUserId));
    }

    [Fact]
    public void GetCart_ExistingCart_ReturnsCartResponse()
    {
        // Arrange
        // First, add a product to create a cart
        CartAddRequest initialRequest = new()
        {
            UserId = _sampleUserId,
            ProductId = _sampleProductId,
            Quantity = 1,
        };
        _cartServices.AddToCart(initialRequest);

        // Act
        var response = _cartServices.GetCart(_sampleUserId);

        // Assert
        Assert.NotNull(response);
        Assert.Equal(_sampleUserId, response.UserId);
        Assert.NotEmpty(response.Products);
    }

    [Fact]
    public void RemoveFromCart_NullUserId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? userId = null;
        Guid? productId = _sampleProductId;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartServices.RemoveFromCart(userId, productId));
    }

    [Fact]
    public void RemoveFromCart_NullProductId_ThrowsArgumentNullException()
    {
        // Arrange
        Guid? userId = _sampleUserId;
        Guid? productId = null;

        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => _cartServices.RemoveFromCart(userId, productId));
    }

    [Fact]
    public void RemoveFromCart_NonExistingCart_ThrowsKeyNotFoundException()
    {
        // Arrange
        Guid userId = Guid.NewGuid(); // Cart doesn't exist for this user
        Guid productId = _sampleProductId;

        // Act & Assert
        Assert.Throws<KeyNotFoundException>(() => _cartServices.RemoveFromCart(userId, productId));
    }

    [Fact]
    public void RemoveFromCart_ExistingCartAndProduct_ReturnsTrue()
    {
        // Arrange
        var response = _cartServices.AddToCart(
            new CartAddRequest
            {
                UserId = _sampleUserId,
                ProductId = _sampleProductId,
                Quantity = 1,
            }
        );

        // Act
        bool result = _cartServices.RemoveFromCart(response.UserId, _sampleProductId);

        // Assert
        Assert.True(result);
    }
}
