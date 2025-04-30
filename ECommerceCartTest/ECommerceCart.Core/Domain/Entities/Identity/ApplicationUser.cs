using Microsoft.AspNetCore.Identity;

namespace ECommerceCart.Core.Domain.Entities.Identity;

public class ApplicationUser : IdentityUser<Guid>
{
    public required string PersonName { get; set; }
    public virtual ICollection<Address>? Addresses { get; set; }
}
