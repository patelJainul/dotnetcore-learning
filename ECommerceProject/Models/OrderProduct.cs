namespace Entities;

public class OrderProduct
{
    public Guid OrderId { get; set; }

    public Guid ProductId { get; set; }

    public int Quantity { get; set; }

    public decimal SalePrice { get; set; }

    public decimal ActualPrice { get; set; }
}
