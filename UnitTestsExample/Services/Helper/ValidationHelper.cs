using System.ComponentModel.DataAnnotations;

namespace Services.Helper;

public class ValidationHelper
{
    public static void ModelValidation(object obj)
    {
        var validationContext = new ValidationContext(obj);
        var validationResults = new List<ValidationResult>();
        bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);

        if (!isValid)
        {
            throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
        }
    }
}
