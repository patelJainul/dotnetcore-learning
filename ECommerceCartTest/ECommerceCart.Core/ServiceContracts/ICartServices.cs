using ECommerceCart.Core.DTO;

namespace ECommerceCart.Core.ServiceContracts;

public interface ICartServices
{
    Task<CartResponse?> AddUpdateToCart(AddCartRequest? cartRequest);
    Task<bool> RemoveFromCart(Guid? userId = null, Guid? cartId = null, Guid? productId = null);
    Task<CartResponse?> GetCartProduct(Guid? userId, Guid? cartId, Guid? productId);
    Task<CartResponse?> GetUserCart(Guid? userId);
    Task<CartResponse?> GetCart(Guid? cartId);
    Task<bool> ClearUserCart(Guid? userId);
    Task<CartResponse?> AssignCartToUser(Guid userId, Guid cartId);
}
