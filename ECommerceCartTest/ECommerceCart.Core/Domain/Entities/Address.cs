namespace ECommerceCart.Core.Domain.Entities;

public class Address : BaseModel
{
    public Guid AddressId { get; set; }
    public Guid UserId { get; set; }
    public string AddressLine1 { get; set; } = string.Empty;
    public string AddressLine2 { get; set; } = string.Empty;
}
