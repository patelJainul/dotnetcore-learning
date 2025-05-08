using ECommerceCartCrud.Core.DTO.CartDto;
using ECommerceCartCrud.Core.DTO.ProductDto;

namespace ECommerceCartCrud.UI.ViewModels;

public class ProductViewModel
{
    public List<ProductResponse> products { get; set; } = [];
    public CartResponse? Cart { get; set; }
}
