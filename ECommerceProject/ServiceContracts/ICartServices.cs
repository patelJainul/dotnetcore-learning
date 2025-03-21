using ServiceContracts.DTO.CartDto;

namespace ServiceContracts;

public interface ICartServices
{
    public CartResponse AddToCart(CartAddRequest? cartAddRequest);

    public CartResponse GetCart(Guid? userId);

    public bool RemoveFromCart(Guid? userId, Guid? productId);
}
