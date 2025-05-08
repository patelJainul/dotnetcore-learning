namespace ECommerceCartCrud.Core.DTO.CartDto;

public class RemoveProductFromCartRequest
{
    public Guid CartId { get; set; }
    public Guid? UserId { get; set; }
    public Guid ProductId { get; set; }
}
