using ContactsManager.Core.Enums;
using ContactsManager.Core.Helper;

namespace ContactsManager.Core.Exceptions;

public static class ExceptionExtension
{
    public static object ToJson(this Exception exception)
    {
        object errorObj = new { message = exception.Message };
        return errorObj;
    }

    public static object ToJsonResponse(this Exception exception)
    {
        return JsonResponse<object>.ToJsonResponse(exception.ToJson(), ResponseStatusOptions.Error);
    }
}
