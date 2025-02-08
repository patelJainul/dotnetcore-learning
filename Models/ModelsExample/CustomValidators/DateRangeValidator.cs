using System;
using System.ComponentModel.DataAnnotations;

namespace ModelsExample.CustomValidators;

public class DateRangeValidator : ValidationAttribute
{

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var property = validationContext.ObjectType.GetProperty("FirstName")?.PropertyType.ToString();
        // ?.GetValue(validationContext.ObjectInstance);

        return ValidationResult.Success;
    }

}
