using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel;

public class Role : IdentityRole<int>
{
    public const string DefaultRoleName = "User";
    public const string DefaultRoleNormalizedName = "USER";
    public const int RoleNameMaxLength = 128;


    public ICollection<UserRole> UserRoles { get; set; }
}