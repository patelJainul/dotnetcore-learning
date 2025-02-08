using System;
using System.ComponentModel.DataAnnotations;
using ModelsAssignmetEcommerce.Models;

namespace ModelsAssignmetEcommerce.CustomValidators;

public class ProductsInvoicePriceCompareValidator(string? productsPropertyName) : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        Order order = (Order)validationContext.ObjectInstance;

        List<Product> products = validationContext.ObjectType.GetProperty(productsPropertyName ?? "Products")?.GetValue(order) as List<Product> ?? [];

        if (products.Count == 0)
        {
            return new ValidationResult("Atleast one product is required");
        }

        if (products.Any(p => p.Price < 0) || products.Any(p => p.Quantity <= 0) || products.Any(p => p.ProductCode <= 0))
        {
            return new ValidationResult("Product is invalid");
        }

        double productsTotal = products.Sum(p => p.Price * p.Quantity);

        if (productsTotal != order.InvoicePrice)
        {
            return new ValidationResult("Invoice price should be equal to the sum of the price of all products");
        }

        return ValidationResult.Success;


    }

}
