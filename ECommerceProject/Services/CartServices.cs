using Entities;
using ServiceContracts;
using ServiceContracts.DTO.CartDto;
using ServiceContracts.DTO.CartProductDto;
using Services.Helpers;

namespace Services;

public class CartServices(ICartProductServices cartProductServices) : ICartServices
{
    private readonly List<Cart> _carts = [];
    private readonly ICartProductServices _cartProductServices = cartProductServices;

    public CartResponse AddToCart(CartAddRequest? cartAddRequest)
    {
        ArgumentNullException.ThrowIfNull(cartAddRequest);
        ValidationHelper.ModelValidation(cartAddRequest);

        // Use GuidValidation helper method
        ValidationHelper.GuidValidation(cartAddRequest.UserId, "User ID cannot be empty.");
        ValidationHelper.GuidValidation(cartAddRequest.ProductId, "Product ID cannot be empty.");

        Cart cart;

        try
        {
            cart = GetCart(cartAddRequest.UserId).ToCart();
        }
        catch (KeyNotFoundException)
        {
            cart = new Cart { UserId = cartAddRequest.UserId, CartId = Guid.NewGuid() };
            _carts.Add(cart);
        }

        try
        {
            var cartProduct = _cartProductServices.GetCartProductById(
                cart.CartId,
                cartAddRequest.ProductId
            );

            var cartProductUpdateRequest = new CartProductUpdateRequest
            {
                CartId = cart.CartId,
                ProductId = cartAddRequest.ProductId,
                Quantity = cartAddRequest.Quantity,
            };
            _cartProductServices.UpdateCartProduct(cartProductUpdateRequest);
        }
        catch (KeyNotFoundException)
        {
            var cartProductAddRequest = new CartProductAddRequest
            {
                CartId = cart.CartId,
                ProductId = cartAddRequest.ProductId,
                Quantity = cartAddRequest.Quantity,
            };
            _cartProductServices.AddCartProduct(cartProductAddRequest);
        }

        var cartProducts = _cartProductServices.GetAllCartProducts(cart.CartId);
        return cart.ToCartResponse(cartProducts);
    }

    public CartResponse GetCart(Guid? userId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ValidationHelper.GuidValidation(userId.Value, "User ID cannot be empty.");

        var cart =
            _carts.FirstOrDefault(temp => temp.UserId == userId)
            ?? throw new KeyNotFoundException($"Cart not found for user {userId}");
        var cartProducts = _cartProductServices.GetAllCartProducts(cart.CartId);
        return cart.ToCartResponse(cartProducts);
    }

    public bool RemoveFromCart(Guid? userId, Guid? productId)
    {
        ArgumentNullException.ThrowIfNull(userId);
        ArgumentNullException.ThrowIfNull(productId);
        ValidationHelper.GuidValidation(userId.Value, "User ID cannot be empty.");
        ValidationHelper.GuidValidation(productId.Value, "Product ID cannot be empty.");

        var cart = GetCart(userId);
        return _cartProductServices.DeleteCartProduct(cart.CartId, productId);
    }
}
