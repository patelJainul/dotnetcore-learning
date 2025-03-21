namespace Entities;

public class Order
{
    public Guid OrderId { get; set; }

    public Guid UserId { get; set; }

    public DateTime OrderDate { get; set; }

    public decimal TotalAmount { get; set; }

    public required string Type { get; set; }
}
