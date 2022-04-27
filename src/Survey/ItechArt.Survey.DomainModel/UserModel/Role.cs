using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel.UserModel;

public class Role : IdentityRole<int>
{
    public const int NameMaxLength = 128;

    public const string User = "User";
    public const string Administrator = "Administrator";


    public virtual ICollection<UserRole> UserRoles { get; set; }
}