using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace ItechArt.Survey.DomainModel;

public class Role : IdentityRole<int>
{
    public ICollection<UserRole> UserRoles { get; set; }
}