using Aplication.Interfaces;
using Infraestructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IOrderStatusRepository, OrderStatusRepository>();
        
        return services;
    }
}