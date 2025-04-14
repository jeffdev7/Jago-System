using Jago.domain.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Jago.Infrastructure.DBConfiguration
{
    public interface IAppDbContext { }

    public class ApplicationContext : IdentityDbContext<User>
    {
        public DbSet<Passenger> Passengers { get; set; }
        public DbSet<Trip> Trips { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public ApplicationContext()
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Passenger>().HasKey(_ => _.Id);
            modelBuilder.Entity<Trip>().HasKey(_ => _.Id);
            modelBuilder.Entity<Trip>().HasOne(_ => _.Passenger);

            base.OnModelCreating(modelBuilder);
        }
    }
}
