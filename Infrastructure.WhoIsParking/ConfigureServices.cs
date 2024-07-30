using App.WhoIsParking.Interfaces.Repositories;
using Infrastructure.WhoIsParking.Repositories.EF;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.WhoIsParking;

public static class ConfigureServices
{
    public static void AddInjectionInfrastructure(this IServiceCollection services)
    {
        services.AddScoped<IParkedCarRepository, ParkedCarRepository>();
    }
}
