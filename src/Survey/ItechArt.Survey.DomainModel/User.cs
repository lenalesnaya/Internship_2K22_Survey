using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ItechArt.Survey.DomainModel;

public class User : IdentityUser<int>
{
    public const int UserNameMaxLength = 128;


    public ICollection<UserRole> UserRoles { get; set; }
}