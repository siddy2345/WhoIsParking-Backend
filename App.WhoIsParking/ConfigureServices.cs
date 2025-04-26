using App.WhoIsParking.UseCases.Houses.Commands.Create;
using App.WhoIsParking.UseCases.Houses.Commands.Update;
using App.WhoIsParking.UseCases.ParkedCars.Commands.Create;
using App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;
using MediatR.Pipeline;
using Microsoft.Extensions.DependencyInjection;

namespace App.WhoIsParking;

public static class ConfigureServices
{
    public static void AddInjectionApplication(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.GetAssemblies());
            cfg.AddOpenBehavior(typeof(RequestPreProcessorBehavior<,>));
        });

        services.AddTransient<IRequestPreProcessor<CreateParkedCarCommand>, CreateParkedCarCommandPreProcessor>();
        services.AddTransient<IRequestPreProcessor<GetAllParkedCarsCommand>, GetAllParkedCarsPreProcessor>();

        services.AddTransient<IRequestPreProcessor<CreateHouseCommand>, CreateHouseCommandPreProcessor>();
        services.AddTransient<IRequestPreProcessor<UpdateHouseCommand>, UpdateHouseCommandPreProcessor>();
    }
}
