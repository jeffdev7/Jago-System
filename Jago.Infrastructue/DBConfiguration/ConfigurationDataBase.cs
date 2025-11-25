using Microsoft.Extensions.Configuration;

namespace Jago.Infrastructure.DBConfiguration
{
    internal class ConfigurationDataBase
    {
        public static IConfiguration ConnectionConfiguration
        {
            get
            {

                var path = $"{Directory.GetParent(Directory.GetCurrentDirectory())}\\Infrastructure";
                IConfigurationRoot Configuration = new ConfigurationBuilder()
                    .SetBasePath(path)
                    .AddJsonFile("appsettings.json")
                    .Build();
                return Configuration;
            }
        }
    }
}
