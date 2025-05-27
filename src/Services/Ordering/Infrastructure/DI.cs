using Infrastructure.Data;
using Infrastructure.Data.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DI
{
    public static IServiceCollection AddInfra(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("Database");

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.AddInterceptors(new AuditableEntityInterceptor());
            options.UseSqlServer(connectionString);
        });

        // services.AddScoped<IApplicationDbContext, ApplicationDbContext>();

        return services;
    }
}