using Microsoft.AspNetCore.Identity;

namespace Riode.Models;

public class AppUser : IdentityUser
{
    public string Fullname { get; set; }
    public bool IsActive { get; set; }
}
