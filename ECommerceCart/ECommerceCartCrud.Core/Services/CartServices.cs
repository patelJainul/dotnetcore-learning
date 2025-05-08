using ECommerceCartCrud.Core.Domain.Entities;
using ECommerceCartCrud.Core.Domain.RepositoryContracts;
using ECommerceCartCrud.Core.DTO.CartDto;
using ECommerceCartCrud.Core.Helpers;
using ECommerceCartCrud.Core.ServiceContracts;
using Microsoft.EntityFrameworkCore;

namespace ECommerceCartCrud.Core.Services;

public class CartServices(IRepository<Cart> cartRepository) : ICartServices
{
    public async Task<CartResponse?> AddToCart(AddProductToCartRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ValidationHelper.ValidateModel(request);
        var res = default(Cart?);
        var existCart = await ExistCart(request.CartId);
        if (existCart is not null)
        {
            var product = existCart?.CartProducts?.FirstOrDefault(p =>
                p.ProductId == request.ProductId
            );
            if (product is not null)
            {
                product.Quantity = request.Quantity == 0 ? 1 : request.Quantity;
            }
            else
            {
                existCart!.CartProducts?.Add(request.ToCart().CartProducts!.FirstOrDefault()!);
            }
            res = await cartRepository.Update(existCart!);
            return res?.ToCartResponse();
        }
        res = await cartRepository.Add(request.ToCart());
        return res?.ToCartResponse();
    }

    public async Task<bool> ClearCart(Guid cartId)
    {
        ArgumentNullException.ThrowIfNull(cartId, nameof(cartId));
        ValidationHelper.ValidateGuid(cartId);

        return await cartRepository.Delete(cartId);
    }

    public async Task<List<CartResponse>> GetAll()
    {
        var carts = await cartRepository.GetAll();
        return [.. carts.Select(c => c.ToCartResponse())];
    }

    public async Task<CartResponse?> GetCartById(Guid id)
    {
        var cart = await cartRepository.GetByQuery(
            c => c.CartId == id,
            cart => cart.Include(c => c.CartProducts)!.ThenInclude(cp => cp.Product)
        );
        return cart?.ToCartResponse();
    }

    public async Task<CartResponse?> RemoveProductFromCart(RemoveProductFromCartRequest request)
    {
        ArgumentNullException.ThrowIfNull(request, nameof(request));
        ValidationHelper.ValidateModel(request);
        var cart = await ExistCart(request.CartId);
        if (cart is null || cart.CartProducts is null)
            return null;
        var product = cart.CartProducts?.FirstOrDefault(p => p.ProductId == request.ProductId);
        if (product is null)
            return null;
        cart.CartProducts!.Remove(product);
        var res = await cartRepository.Update(cart);
        return res?.ToCartResponse();
    }

    public async Task<Cart?> ExistCart(Guid? cartId)
    {
        try
        {
            ArgumentNullException.ThrowIfNull(cartId, nameof(cartId));
            ValidationHelper.ValidateGuid(cartId);
            var cart = await cartRepository.GetByQuery(
                c => c.CartId == cartId,
                cart => cart.Include(c => c.CartProducts)
            );
            if (cart is null)
                return null;
            return cart;
        }
        catch
        {
            return null;
        }
    }
}
