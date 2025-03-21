using ServiceContracts.DTO.CartProductDto;

namespace ServiceContracts;

public interface ICartProductServices
{
    public CartProductResponse AddCartProduct(CartProductAddRequest? cartProductAddRequest);

    public List<CartProductResponse> GetAllCartProducts(Guid? cartId);

    public CartProductResponse GetCartProductById(Guid? cartId, Guid? productId);

    public CartProductResponse UpdateCartProduct(
        CartProductUpdateRequest? cartProductUpdateRequest
    );

    public bool DeleteCartProduct(Guid? cartId, Guid? productId);
}
