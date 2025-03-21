using System.ComponentModel.DataAnnotations;

namespace Services.Helpers;

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

    public static void GuidValidation(Guid? guid, string errorMessage)
    {
        if (guid == null || guid == Guid.Empty || Guid.TryParse(guid.ToString(), out _) == false)
        {
            throw new ArgumentException(errorMessage);
        }
    }
}
