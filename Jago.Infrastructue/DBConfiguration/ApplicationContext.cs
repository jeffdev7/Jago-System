using Jago.domain.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Jago.Infrastructure.DBConfiguration
{
    public interface IAppDbContext { }
   // IdentityDbContext<ApplicationUser>, IAppDbContext
    public class ApplicationContext : DbContext
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public ApplicationContext()
        {

        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                IConfigurationRoot config = new ConfigurationBuilder()
                    .SetBasePath(Directory.GetCurrentDirectory())
                    .AddJsonFile("appsettings.json")
                    .Build();
                var conn = config.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(conn);
            }
        }

    }
}

/*(options => options.UseSqlServer(Configuration.GetConnectionString("ConnectionDBJago")))*/
