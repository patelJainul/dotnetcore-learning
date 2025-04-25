using ECommerceCart.Core.Domain.Entities;
using ECommerceCart.Core.Domain.RepositoryContracts;
using ECommerceCart.Core.DTO;
using ECommerceCart.Core.Helpers;
using ECommerceCart.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCart.Core.Services;

public class CartServices(
    IRepository<Cart> _cartRepository,
    IRepository<CartVsProducts> _cartVsProductRepository
) : ICartServices
{
    public async Task<CartResponse?> AddUpdateToCart(AddCartRequest? cartRequest)
    {
        ArgumentNullException.ThrowIfNull(cartRequest, nameof(cartRequest));
        ValidationHelper.ModelValidation(cartRequest);

        var existingCart = await GetCartProduct(
            productId: cartRequest.ProductId,
            cartId: cartRequest.CartId
        );

        CartVsProducts newCartVsProduct;

        if (existingCart != null)
        {
            var existingCartVsProducts = await _cartVsProductRepository.GetByQuery(cp =>
                cp.CartId == existingCart.CartId && cp.ProductId == cartRequest.ProductId
            );

            if (existingCartVsProducts != null)
            {
                if (cartRequest.Quantity == 0)
                {
                    await _cartVsProductRepository.Delete(existingCartVsProducts.CartId);
                }
                else
                {
                    existingCartVsProducts.Quantity = cartRequest.Quantity;
                    await _cartVsProductRepository.Update(existingCartVsProducts);
                }
            }
            else
            {
                newCartVsProduct = cartRequest.ToCart().CartVsProducts.FirstOrDefault()!;
                await _cartVsProductRepository.Add(newCartVsProduct);
            }

            return await GetCart(existingCart.CartId);
        }

        var newCart = cartRequest.ToCart();
        await _cartRepository.Add(newCart);

        return await GetCart(newCart.CartId);
    }

    public async Task<CartResponse?> AssignCartToUser(Guid userId, Guid cartId)
    {
        var cart = await _cartRepository.GetById(cartId);
        if (cart == null)
            return null;
        if (cart.UserId != null)
            return null;
        cart.UserId = userId;
        return (await _cartRepository.Update(cart))?.ToCartResponse();
    }

    public async Task<bool> ClearUserCart(Guid? userId)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));
        ValidationHelper.ValidateGuid(userId.Value, nameof(userId));
        return await _cartRepository.Delete(userId.Value);
    }

    public async Task<CartResponse?> GetCart(Guid? cartId)
    {
        ArgumentNullException.ThrowIfNull(cartId, nameof(cartId));
        ValidationHelper.ValidateGuid(cartId.Value, nameof(cartId));
        return (
            await _cartRepository.GetByQuery(c =>
                c.Include(c => c.CartVsProducts).ThenInclude(cp => cp.Product)
            )
        )?.ToCartResponse();
    }

    public async Task<CartResponse?> GetCartProduct(
        Guid? userId = null,
        Guid? cartId = null,
        Guid? productId = null
    )
    {
        ArgumentNullException.ThrowIfNull(productId, nameof(productId));
        ValidationHelper.ValidateGuid(productId.Value, nameof(productId));
        if (userId == null && cartId == null)
        {
            return null;
        }
        if (userId != null)
        {
            var cartData = await GetUserCart(userId);
            if (cartData == null)
                return null;
            cartData.CartVsProducts =
            [
                .. cartData.CartVsProducts.Where(cp => cp?.Product?.ProductId == productId),
            ];
            return cartData;
        }
        else
        {
            var cartData = await GetCart(cartId);
            if (cartData == null)
                return null;
            cartData.CartVsProducts =
            [
                .. cartData.CartVsProducts.Where(cp => cp?.Product?.ProductId == productId),
            ];
            return cartData;
        }
    }

    public async Task<CartResponse?> GetUserCart(Guid? userId)
    {
        ArgumentNullException.ThrowIfNull(userId, nameof(userId));
        ValidationHelper.ValidateGuid(userId.Value, nameof(userId));
        var cartData = await _cartRepository.GetByQuery(
            c => c.UserId == userId,
            [c => c.Include(c => c.CartVsProducts).ThenInclude(cp => cp.Product)]
        );
        return cartData?.ToCartResponse();
    }

    public async Task<bool> RemoveFromCart(
        Guid? userId = null,
        Guid? cartId = null,
        Guid? productId = null
    )
    {
        var cartProduct = await GetCartProduct(
            userId: userId,
            cartId: cartId,
            productId: productId
        );
        if (cartProduct == null)
            return false;
        var cartProductId = cartProduct
            .CartVsProducts.FirstOrDefault(cp => cp.Product?.ProductId == productId)
            ?.CartVsProductsId;
        if (cartProductId == null)
            return false;
        return await _cartVsProductRepository.Delete(cartProductId.Value);
    }
}
