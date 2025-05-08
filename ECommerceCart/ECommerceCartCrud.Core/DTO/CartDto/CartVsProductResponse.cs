using ECommerceCartCrud.Core.Domain.Entities;

namespace ECommerceCartCrud.Core.DTO.CartDto;

public class CartVsProductResponse
{
    public Guid CartVsProductId { get; set; }
    public Guid ProductId { get; set; }
    public required string Name { get; set; }
    public string? Description { get; set; }
    public double Price { get; set; }
    public int Quantity { get; set; } = 1;
}

public static class CartVsProductResponseExtensions
{
    public static CartVsProductResponse ToCartVsProductResponse(this CartVsProduct cartVsProduct)
    {
        return new CartVsProductResponse()
        {
            CartVsProductId = cartVsProduct.CartVsProductId,
            ProductId = cartVsProduct.ProductId,
            Name = cartVsProduct.Product?.Name ?? string.Empty,
            Description = cartVsProduct.Product?.Description,
            Price = cartVsProduct.Product?.Price ?? 0,
            Quantity = cartVsProduct.Quantity,
        };
    }
}
