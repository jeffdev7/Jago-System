using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Jago.Infrastructure.DBConfiguration
{
    public class IdentityDbContext<TUser> : IdentityDbContext<TUser, IdentityRole, string> where TUser: IdentityUser
    {
        public IdentityDbContext(DbContextOptions options) { }
        protected IdentityDbContext() { }
    }
}
