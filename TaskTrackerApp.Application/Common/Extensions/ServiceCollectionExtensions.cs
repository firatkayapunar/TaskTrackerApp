using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using TaskTrackerApp.Application.Common.Interfaces;
using TaskTrackerApp.Application.Common.MappingProfiles;
using TaskTrackerApp.Application.Common.Marker;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<ApplicationAssemblyMarker>();

        services.AddAutoMapper(cfg =>
        {
            cfg.AddProfile<TaskItemMappingProfile>();
            cfg.AddProfile<UserMappingProfile>();
        });

        return services;
    }
}
