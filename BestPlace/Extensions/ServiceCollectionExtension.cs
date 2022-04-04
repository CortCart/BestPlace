using BestPlace.Core.Contracts;
using BestPlace.Core.Services;
using BestPlace.Infrastructure.Data;
using BestPlace.Infrastructure.Data.Identity;
using BestPlace.Infrastructure.Data.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BestPlace.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplicationService(this IServiceCollection services)
    {
        services.AddScoped<IApplicatioDbRepository, ApplicatioDbRepository>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IStatisticsService, StatisticsService>();
        services.AddScoped<IItemService, ItemService>();
        services.AddScoped<ICategoryService, CategoryService>();
        services.AddScoped<IDealService, DealService>();
        services.AddScoped<IQuestionnaireService, QuestionnaireService>();
        services.AddScoped<IRoleService, RoleService>();
        services.AddScoped<IImageService, ImageService>();

        // services.AddRazorPages();
        //.AddRazorPagesOptions(options =>
        //{
        //    options.Conventions.AddAreaPageRoute("Admin", "/Info", "Admin/Home/Info");
        //});


        return services;
    }
    public static IServiceCollection AddApplicationDbContexts(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("BestPlaceContextConnection");
        services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddDatabaseDeveloperPageExceptionFilter();

        return services;
    }
    
    public static IServiceCollection AddApplicationIdentity(this IServiceCollection services)
    {
        services.AddDefaultIdentity<ApplicationUser>(options =>
            {
                options.SignIn.RequireConfirmedAccount = true;
                options.Password.RequireDigit = true;
                options.Password.RequireUppercase = true;
                options.Password.RequireLowercase = true;
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            ;
        return services;
    }
}