using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Repositories;
using TaskTrackerApp.Infrastructure.Auth;
using TaskTrackerApp.Infrastructure.Common.Marker;
using TaskTrackerApp.Infrastructure.Persistence;
using TaskTrackerApp.Infrastructure.Persistence.Interceptors;
using TaskTrackerApp.Infrastructure.Persistence.Repositories;

namespace TaskTrackerApp.Infrastructure.Common.Extensions;

public static class InfrastructureExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();

        services.AddScoped<AuditDbContextInterceptor>();

        services.AddDbContext<ApplicationDbContext>((serviceProvider, options) =>
        {
            var connectionStrings = configuration.GetSection(ConnectionStringOptions.Key).Get<ConnectionStringOptions>();

            options.UseSqlServer(connectionStrings!.SqlServer,
                sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(InfrastructureAssemblyMarker).Assembly.FullName);
                });

            var interceptor = serviceProvider.GetRequiredService<AuditDbContextInterceptor>();

            options.AddInterceptors(interceptor);
        });

        services.AddScoped<ITaskItemRepository, TaskItemRepository>();

        services.AddScoped<IUserRepository, UserRepository>();

        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        services.AddScoped<IJwtTokenService, JwtTokenService>();

        services.AddScoped<IPasswordHasher, BcryptPasswordHasher>();

        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
