using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models;

// Extends ASP.NET Core Identity's IdentityUser with custom profile fields
public class AppUser : IdentityUser
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
}
