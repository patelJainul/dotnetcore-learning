using ECommerceCartCrud.Core.Domain.Entities;
using ECommerceCartCrud.Core.Domain.RepositoryContracts;
using ECommerceCartCrud.Core.DTO.ProductDto;
using ECommerceCartCrud.Core.Helpers;
using ECommerceCartCrud.Core.ServiceContracts;

namespace ECommerceCartCrud.Core.Services;

public class ProductServices(IRepository<Product> productRepository) : IProductServices
{
    public async Task<List<ProductResponse>> GetAllProducts()
    {
        var res = await productRepository.GetAll();
        return [.. res.Select(p => p.ToProductResponse())];
    }

    public async Task<ProductResponse?> GetProductById(Guid id)
    {
        ArgumentNullException.ThrowIfNull(id, nameof(id));
        ValidationHelper.ValidateGuid(id);
        var res = await productRepository.GetById(id);
        return res?.ToProductResponse();
    }
}
