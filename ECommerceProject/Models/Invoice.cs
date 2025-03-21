namespace Entities;

public class Invoice
{
    public Guid InvoiceId { get; set; }

    public Guid OrderId { get; set; }

    public DateTime InvoiceDate { get; set; }

    public decimal TotalAmount { get; set; }
}
