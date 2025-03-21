using Entities;
using ServiceContracts;
using ServiceContracts.DTO.ProductDto;
using Services.Helpers;

namespace Services;

public class ProductServices(ICategoryServices categoryServices) : IProductServices
{
    private readonly List<Product> _products = [];
    private readonly ICategoryServices _categoryServices = categoryServices;

    public ProductResponse AddProduct(ProductAddRequest? productAddRequest)
    {
        ArgumentNullException.ThrowIfNull(productAddRequest);
        ValidationHelper.ModelValidation(productAddRequest);

        var product = productAddRequest.ToProduct();
        _products.Add(product);

        return product.ToProductResponse(_categoryServices.GetCategoryById(product.CategoryId));
    }

    public List<ProductResponse> GetAllProducts()
    {
        return
        [
            .. _products.Select(product =>
                product.ToProductResponse(_categoryServices.GetCategoryById(product.CategoryId))
            ),
        ];
    }

    public ProductResponse GetProductById(Guid? productId)
    {
        ArgumentNullException.ThrowIfNull(productId);

        var product =
            _products.FirstOrDefault(product => product.ProductId == productId)
            ?? throw new KeyNotFoundException($"Product with id {productId} not found");

        return product.ToProductResponse(_categoryServices.GetCategoryById(product.CategoryId));
    }

    public ProductResponse UpdateProduct(ProductUpdateRequest? productUpdateRequest)
    {
        ArgumentNullException.ThrowIfNull(productUpdateRequest);
        ValidationHelper.ModelValidation(productUpdateRequest);

        var product =
            _products.FirstOrDefault(product => product.ProductId == productUpdateRequest.ProductId)
            ?? throw new KeyNotFoundException(
                $"Product with id {productUpdateRequest.ProductId} not found"
            );

        var updatedProduct = productUpdateRequest.ToUpdatedProduct(product);
        _products.Remove(product);
        _products.Add(updatedProduct);

        return updatedProduct.ToProductResponse(
            _categoryServices.GetCategoryById(updatedProduct.CategoryId)
        );
    }

    public bool DeleteProduct(Guid? productId)
    {
        ArgumentNullException.ThrowIfNull(productId);

        var product =
            _products.FirstOrDefault(product => product.ProductId == productId)
            ?? throw new KeyNotFoundException($"Product with id {productId} not found");

        var isRemoved = _products.Remove(product);

        if (!isRemoved)
        {
            throw new Exception("Failed to remove product");
        }

        return isRemoved;
    }
}
