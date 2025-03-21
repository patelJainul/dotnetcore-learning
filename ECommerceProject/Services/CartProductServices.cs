using Entities;
using ServiceContracts;
using ServiceContracts.DTO.CartProductDto;
using Services.Helpers;

namespace Services;

public class CartProductServices(IProductServices productServices) : ICartProductServices
{
    private readonly List<CartProduct> _cartProducts = [];
    private readonly IProductServices _productServices = productServices;

    public CartProductResponse AddCartProduct(CartProductAddRequest? cartProductAddRequest)
    {
        ArgumentNullException.ThrowIfNull(cartProductAddRequest);
        ValidationHelper.ModelValidation(cartProductAddRequest);

        ValidationHelper.GuidValidation(cartProductAddRequest.CartId, "Cart ID cannot be empty.");
        ValidationHelper.GuidValidation(
            cartProductAddRequest.ProductId,
            "Product ID cannot be empty."
        );

        var cartProduct = cartProductAddRequest.ToCartProduct();
        _cartProducts.Add(cartProduct);
        var product = _productServices.GetProductById(cartProduct.ProductId);

        return cartProduct.ToCartProductResponse(product);
    }

    public List<CartProductResponse> GetAllCartProducts(Guid? cartId)
    {
        ArgumentNullException.ThrowIfNull(cartId);
        ValidationHelper.GuidValidation(cartId.Value, "Cart ID cannot be empty.");

        var cartProducts = _cartProducts
            .Where(cartProduct => cartProduct.CartId == cartId)
            .Select(cartProduct =>
                cartProduct.ToCartProductResponse(
                    _productServices.GetProductById(cartProduct.ProductId)
                )
            )
            .ToList();

        return cartProducts;
    }

    public CartProductResponse GetCartProductById(Guid? cartId, Guid? productId)
    {
        ArgumentNullException.ThrowIfNull(cartId);
        ArgumentNullException.ThrowIfNull(productId);

        ValidationHelper.GuidValidation(cartId.Value, "Cart ID cannot be empty.");
        ValidationHelper.GuidValidation(productId.Value, "Product ID cannot be empty.");

        var cartProduct =
            _cartProducts.FirstOrDefault(cartProduct =>
                cartProduct.CartId == cartId && cartProduct.ProductId == productId
            )
            ?? throw new KeyNotFoundException(
                $"Cart product with cart id {cartId} and product id {productId} not found"
            );

        return cartProduct.ToCartProductResponse(
            _productServices.GetProductById(cartProduct.ProductId)
        );
    }

    public CartProductResponse UpdateCartProduct(CartProductUpdateRequest? cartProductUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(cartProductUpdateRequest);
        ValidationHelper.ModelValidation(cartProductUpdateRequest);

        ValidationHelper.GuidValidation(
            cartProductUpdateRequest.CartId,
            "Cart ID cannot be empty."
        );
        ValidationHelper.GuidValidation(
            cartProductUpdateRequest.ProductId,
            "Product ID cannot be empty."
        );

        var cartProduct =
            _cartProducts.FirstOrDefault(cartProduct =>
                cartProduct.CartId == cartProductUpdateRequest.CartId
                && cartProduct.ProductId == cartProductUpdateRequest.ProductId
            )
            ?? throw new KeyNotFoundException(
                $"Cart product with cart id {cartProductUpdateRequest.CartId} and product id {cartProductUpdateRequest.ProductId} not found"
            );

        var updatedCartProduct = cartProductUpdateRequest.ToUpdatedCartProduct(cartProduct);
        _cartProducts.Remove(cartProduct);
        _cartProducts.Add(updatedCartProduct);

        return cartProduct.ToCartProductResponse(
            _productServices.GetProductById(cartProduct.ProductId)
        );
    }

    public bool DeleteCartProduct(Guid? cartId, Guid? productId)
    {
        ArgumentNullException.ThrowIfNull(cartId);
        ArgumentNullException.ThrowIfNull(productId);

        ValidationHelper.GuidValidation(cartId.Value, "Cart ID cannot be empty.");
        ValidationHelper.GuidValidation(productId.Value, "Product ID cannot be empty.");

        var cartProduct =
            _cartProducts.FirstOrDefault(cartProduct =>
                cartProduct.CartId == cartId && cartProduct.ProductId == productId
            )
            ?? throw new KeyNotFoundException(
                $"Cart product with cart id {cartId} and product id {productId} not found"
            );
        var isRemoved = _cartProducts.Remove(cartProduct);

        if (!isRemoved)
        {
            throw new Exception("Failed to remove cart product");
        }

        return isRemoved;
    }
}
