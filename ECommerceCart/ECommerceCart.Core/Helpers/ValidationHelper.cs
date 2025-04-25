using System.ComponentModel.DataAnnotations;

namespace ECommerceCart.Core.Helpers;

public class ValidationHelper
{
    // ModelValidation
    public static void ModelValidation(object model)
    {
        if (model == null)
        {
            throw new ArgumentNullException(nameof(model), "Model cannot be null.");
        }

        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(
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

    public static void ValidateGuid(Guid id, string paramName)
    {
        if (id == Guid.Empty)
        {
            throw new ArgumentException($"Invalid {paramName}: {id}", paramName);
        }
    }
}
