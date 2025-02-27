using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.DBConfiguration
{
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser : IdentityUser
    {
        public IdentityDbContext(DbContextOptions options) { }
        protected IdentityDbContext() { }
    }
}
