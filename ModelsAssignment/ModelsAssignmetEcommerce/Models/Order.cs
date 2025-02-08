using System.ComponentModel.DataAnnotations;
using ModelsAssignmetEcommerce.CustomValidators;

namespace ModelsAssignmetEcommerce.Models;

public class Order
{
    public int? OrderNo { get; set; } = new Random().Next(10000, 99999);

    [Required(ErrorMessage = "OrderDate can't be blank")]
    public DateTime? OrderDate { get; set; }

    [ProductsInvoicePriceCompareValidator("Products", ErrorMessage = "Invoice price should be equal to the sum of the price of all products")]
    public double InvoicePrice { get; set; }

    [Required]
    public List<Product>? Products { get; set; }
}
