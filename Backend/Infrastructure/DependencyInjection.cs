using Aplication.Interfaces;
using Aplication.Interfaces.Locations;
using Aplication.Interfaces.Medias;
using Aplication.Services;
using Infraestructure.Repositories;
using Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        services.AddScoped<IOrderStatusService, OrderStatusService>();

        services.AddScoped<ILocationRepository, LocationRepository>();
        services.AddScoped<ILocationService, LocationService>();

        services.AddScoped<IMediaRepository, MediaRepository>();
        services.AddScoped<IMediaService, MediaService>();
        
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();
        services.AddScoped<IUserRoleService, UserRoleService>();

        return services;
    }
}