using ContactsManager.Core.Enums;

namespace ContactsManager.Core.Helpers;

public class JsonResponse<T>
{
    public string Status { get; set; } = ResponseStatusOptions.Success.ToString();
    public T? Data { get; set; } = default!;

    public string? Message { get; set; } = string.Empty;

    public static JsonResponse<T> ToJsonResponse(
        T? data = default,
        string? message = "",
        ResponseStatusOptions status = ResponseStatusOptions.Success
    )
    {
        return new JsonResponse<T>
        {
            Status = status.ToString(),
            Data = data,
            Message = message,
        };
    }
}

public static class ListExtension
{
    public static JsonResponse<List<T>> ToJsonResponse<T>(
        this List<T> data,
        string? message = "",
        ResponseStatusOptions status = ResponseStatusOptions.Success
    )
    {
        return new JsonResponse<List<T>>
        {
            Status = status.ToString(),
            Data = data,
            Message = message,
        };
    }
}
