using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.DomainModel.UserModel;

public class UserRole : IdentityUserRole<long>
{
    public virtual User User { get; set; }
    public long UserId { get; set; }

    public virtual Role Role { get; set; }
    public long RoleId { get; set; }
}