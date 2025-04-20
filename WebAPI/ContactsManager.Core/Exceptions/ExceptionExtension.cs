using ContactsManager.Core.Enums;
using ContactsManager.Core.Helpers;

namespace ContactsManager.Core.Exceptions;

public static class ExceptionExtension
{
    public static object ToJsonResponse(this Exception exception)
    {
        return JsonResponse<object>.ToJsonResponse(
            status: ResponseStatusOptions.Error,
            message: exception.Message
        );
    }
}
