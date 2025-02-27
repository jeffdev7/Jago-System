using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace Jago.Infrastructure.DBConfiguration
{
    public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<ApplicationContext>
    {
        public ApplicationContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json")
               .Build();
            var builder = new DbContextOptionsBuilder<ApplicationContext>();
            var connection = config.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(connection);

            return new ApplicationContext(builder.Options);
        }
    }
}
