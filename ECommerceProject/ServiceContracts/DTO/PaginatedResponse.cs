namespace ServiceContracts.DTO;

public class PaginatedResponse<T>
{
    public List<T> Items { get; set; } = [];
    public int TotalItems { get; set; }
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
