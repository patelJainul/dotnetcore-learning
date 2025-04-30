using ECommerceCart.Core.DTO;

namespace ECommerceCart.Core.ServiceContracts;

public interface IProductServices
{
    Task<ProductResponse> AddProduct(AddProductRequest product);
    Task<ProductResponse> UpdateProduct(UpdateProductRequest product);
    Task<bool> DeleteProduct(Guid productId);
    Task<ProductResponse?> GetProductById(Guid productId);
    Task<List<ProductResponse>> GetAllProducts();
    Task<List<ProductResponse>> GetProductsByCategory(string categoryName);
    Task<List<ProductResponse>> SearchProducts(string searchTerm);
}
