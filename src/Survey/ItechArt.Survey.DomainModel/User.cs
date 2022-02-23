using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.DomainModel;

public class User : IdentityUser<int>
{
    [StringLength(128, ErrorMessage = "User name cannot be longer than 50 characters.")]
    public override string UserName { get; set; }

    public ICollection<UserRole> UserRoles { get; set; }
}