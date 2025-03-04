using Microsoft.AspNetCore.Identity;

namespace Jago.domain.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
