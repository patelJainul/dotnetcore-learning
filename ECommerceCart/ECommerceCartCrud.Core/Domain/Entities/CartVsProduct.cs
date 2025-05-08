using System.ComponentModel.DataAnnotations.Schema;

namespace ECommerceCartCrud.Core.Domain.Entities;

public class CartVsProduct
{
    public Guid CartVsProductId { get; set; }
    public Guid CartId { get; set; }
    public Guid ProductId { get; set; }
    public int Quantity { get; set; } = 1;
    public virtual Product? Product { get; set; }
}
