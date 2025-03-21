using ServiceContracts.DTO;

namespace Services.Helpers;

public class PaginatedResponseHelper
{
    public static PaginatedResponse<T> CreatePaginatedResponse<T>(
        IEnumerable<T> items,
        int totalItems,
        int pageNumber,
        int pageSize
    )
    {
        return new PaginatedResponse<T>
        {
            Items = [.. items],
            TotalItems = totalItems,
            PageNumber = pageNumber,
            PageSize = pageSize,
        };
    }
}
