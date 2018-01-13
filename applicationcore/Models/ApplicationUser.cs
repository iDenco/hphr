using Microsoft.AspNetCore.Identity;

namespace HPHR.ApplicationCore.Models
{
  public class ApplicationUser: IdentityUser
    {
        public string GivenName { get; set; }
    }
}
