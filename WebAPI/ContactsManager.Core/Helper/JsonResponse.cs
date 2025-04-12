using ContactsManager.Core.Enums;

namespace ContactsManager.Core.Helper;

public class JsonResponse<T>
{
    public string Status { get; set; } = ResponseStatusOptions.Success.ToString();
    public T Data { get; set; } = default!;

    public static JsonResponse<T> ToJsonResponse(
        T data,
        ResponseStatusOptions status = ResponseStatusOptions.Success
    )
    {
        return new JsonResponse<T> { Status = status.ToString(), Data = data };
    }
}

public static class ListExtension
{
    public static JsonResponse<List<T>> ToJsonResponse<T>(
        this List<T> data,
        ResponseStatusOptions status = ResponseStatusOptions.Success
    )
    {
        return new JsonResponse<List<T>> { Status = status.ToString(), Data = data };
    }
}
