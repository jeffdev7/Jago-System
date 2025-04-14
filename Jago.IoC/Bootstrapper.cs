using Jago.Application.Interfaces.Services;
using Jago.Application.Services;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Jago.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jago.IoC
{
    public class Bootstrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {

            //Repo
            services.AddScoped<IPassengerRepository, PassengerRepository>();
            services.AddScoped<ITripRepository, TripRepository>();


            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            });

            //Application: Service
            services.AddScoped<IPassengerServices, PassengerServices>();
            services.AddScoped<ITripServices, TripServices>();
            services.AddScoped<IUserServices, UserServices>();

            services.AddDbContext<ApplicationContext>();
            services.AddScoped<HttpContextAccessor>();
            //services.AddTransient<IAppDbContext, ApplicationContext>();
        }
    }
}
