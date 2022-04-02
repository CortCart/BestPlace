using BestPlace.Core.Contracts;
using BestPlace.Core.Services;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.EntityFrameworkCore;



namespace Microsoft.Extensions.DependencyInjection
{
    public static class ApiServiceCollectionExtension
    {
        public static IServiceCollection AddApiServices(this IServiceCollection services)
        {
            services.AddScoped<IApplicatioDbRepository, ApplicatioDbRepository>();
            services.AddScoped<IImageService, ImageService>();

            return services;
        }

        public static IServiceCollection AddApiDbContexts(this IServiceCollection services, IConfiguration config)
        {
            var connectionString = config.GetConnectionString("BestPlaceContextConnection");
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));

            return services;
        }
    }
}