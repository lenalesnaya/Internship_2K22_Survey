using Microsoft.AspNetCore.Identity;

namespace ItechArt.Survey.DomainModel.UserModel;

public class UserRole : IdentityUserRole<int>
{
    public User User { get; set; }

    public Role Role { get; set; }
}