namespace ECommerceCart.Core.Domain.Entities;

public class Cart : BaseModel
{
    public Guid CartId { get; set; }
    public Guid? UserId { get; set; }
    public virtual ICollection<CartVsProducts> CartVsProducts { get; set; } = [];
}
