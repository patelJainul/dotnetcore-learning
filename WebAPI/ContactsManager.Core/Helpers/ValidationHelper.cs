using System.ComponentModel.DataAnnotations;

namespace ContactsManager.Core.Helpers;

public class ValidationHelper
{
    public static void ModelValidation(object model)
    {
        var validationContext = new ValidationContext(model);
        var validationResults = new List<ValidationResult>();

        if (!Validator.TryValidateObject(model, validationContext, validationResults, true))
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

    public static void StringValidation(string? str, string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(str))
        {
            throw new ArgumentException(errorMessage);
        }
    }
}
