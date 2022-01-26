using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
