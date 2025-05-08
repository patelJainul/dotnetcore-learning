using ECommerceCartCrud.Core.DTO.ProductDto;

namespace ECommerceCartCrud.Core.ServiceContracts;

public interface IProductServices
{
    Task<List<ProductResponse>> GetAllProducts();
    Task<ProductResponse?> GetProductById(Guid id);
}
