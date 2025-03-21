using ServiceContracts.DTO.ProductDto;

namespace ServiceContracts;

public interface IProductServices
{
    public ProductResponse AddProduct(ProductAddRequest? productAddRequest);

    public List<ProductResponse> GetAllProducts();

    public ProductResponse GetProductById(Guid? productId);

    public ProductResponse UpdateProduct(ProductUpdateRequest? productUpdateRequest);

    public bool DeleteProduct(Guid? productId);
}
