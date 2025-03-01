﻿using Jago.Application.Services;
using Jago.domain.Interfaces.Repositories;
using Jago.Infrastructure.DBConfiguration;
using Jago.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Jago.IoC
{
    public class Bootstrapper
    {
        //  public IConfiguration Config { get; }
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

            services.AddDbContext<ApplicationContext>();
            //services.AddTransient<IAppDbContext, ApplicationContext>();
        }
    }
}
