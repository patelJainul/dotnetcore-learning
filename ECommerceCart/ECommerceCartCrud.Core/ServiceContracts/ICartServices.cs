using ECommerceCartCrud.Core.DTO.CartDto;

namespace ECommerceCartCrud.Core.ServiceContracts;

public interface ICartServices
{
    Task<CartResponse?> AddToCart(AddProductToCartRequest request);
    Task<List<CartResponse>> GetAll();
    Task<CartResponse?> GetCartById(Guid id);
    Task<CartResponse?> RemoveProductFromCart(RemoveProductFromCartRequest request);
    Task<bool> ClearCart(Guid cartId);
}
