using ECommerceCart.Core.Domain.Entities;
using ECommerceCart.Core.Domain.RepositoryContracts;
using ECommerceCart.Core.DTO;
using ECommerceCart.Core.Helpers;
using ECommerceCart.Core.ServiceContracts;

namespace ECommerceCart.Core.Services;

public class ProductServices(IRepository<Product> _productRepository) : IProductServices
{
    public async Task<ProductResponse> AddProduct(AddProductRequest product)
    {
        ValidationHelper.ModelValidation(product);
        var newProduct = product.ToProduct();
        var addedProduct = await _productRepository.Add(newProduct);
        return addedProduct.ToProductResponse();
    }

    public Task<bool> DeleteProduct(Guid productId)
    {
        throw new NotImplementedException();
    }

    public async Task<List<ProductResponse>> GetAllProducts()
    {
        var products = await _productRepository.GetAll();
        return [.. products.Select(p => p.ToProductResponse())];
    }

    public Task<ProductResponse?> GetProductById(Guid productId)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> GetProductsByCategory(string categoryName)
    {
        throw new NotImplementedException();
    }

    public Task<List<ProductResponse>> SearchProducts(string searchTerm)
    {
        throw new NotImplementedException();
    }

    public Task<ProductResponse> UpdateProduct(UpdateProductRequest product)
    {
        throw new NotImplementedException();
    }
}
