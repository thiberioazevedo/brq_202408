using DDD.Infra.Data.Context;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DDD.Services.Api.StartupExtensions
{
    public static class DatabaseExtension
    {
        public static IServiceCollection AddCustomizedDatabase(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment env)
        {
            services.AddDbContext<ApplicationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("ApplicationConnection"));
                // options.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
                if (!env.IsProduction())
                {
                    options.EnableDetailedErrors();
                    options.EnableSensitiveDataLogging();
                }
            });

            return services;
        }
    }
}
