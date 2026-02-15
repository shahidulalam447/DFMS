using Firm.Infrastructure.Auth;
using Firm.Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Firm.Infrastructure
{
    public static class IocContainer
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddDbContext<FirmDBContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("constring"));
                option.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
            }, ServiceLifetime.Transient);

            services.AddIdentity<ApplicationUser, IdentityRole>(
             options =>
             {
                 options.Password.RequireDigit = false;
                 options.Password.RequiredLength = 6;
                 options.Password.RequireLowercase = false;
                 options.Password.RequireUppercase = false;
                 //options.Password.RequireNonAlphanumeric = false;
                 options.SignIn.RequireConfirmedEmail = false;
             }).AddEntityFrameworkStores<FirmDBContext>().AddDefaultTokenProviders();

            services.Configure<SecurityStampValidatorOptions>(options =>
            {
                options.ValidationInterval = TimeSpan.FromMinutes(50); // 30 minutes is default
            });
            //services.AddScoped(typeof(IEntityRepository<>), typeof(EntityRepository<>));
            services.AddScoped<IWorkContext, WorkContextsService>();
            //services.AddScoped<ICowService, CowService>();
            services.AddHttpContextAccessor();
            return services;
        }
    }
}
