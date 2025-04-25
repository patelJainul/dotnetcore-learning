using ECommerceCart.Core.DTO;

namespace ECommerceCart.Core.ViewModels;

public class ProductsViewModel
{
    public List<ProductResponse> Products { get; set; } = [];
    public CartResponse? Cart { get; set; }
    public string ViewMode { get; set; } = "Card";
}
