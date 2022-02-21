using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel;

public class User : IdentityUser<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}