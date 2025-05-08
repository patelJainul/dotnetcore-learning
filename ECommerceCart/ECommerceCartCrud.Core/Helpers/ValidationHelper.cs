using System.ComponentModel.DataAnnotations;

namespace ECommerceCartCrud.Core.Helpers;

public class ValidationHelper
{
    public static void ValidateModel(object model)
    {
        ArgumentNullException.ThrowIfNull(model, nameof(model));
        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(
            model,
            validationContext,
            validationResults,
            true
        );
        if (!isValid)
        {
            var errors = string.Join(", ", validationResults.Select(vr => vr.ErrorMessage));
            throw new ValidationException($"Model validation failed: {errors}");
        }
    }

    public static void ValidateGuid(Guid? id)
    {
        if (id == null || id == Guid.Empty)
        {
            throw new ArgumentException("Invalid GUID provided.", nameof(id));
        }
    }
}
