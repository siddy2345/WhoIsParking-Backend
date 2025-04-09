using App.WhoIsParking.Interfaces.Repositories;
using Domain.WhoIsParking.Models;
using Infrastructure.WhoIsParking.Repositories.EF;
using Infrastructure.WhoIsParking.Services.EmailSender;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.WhoIsParking;

public static class ConfigureServices
{
    public static void AddInjectionInfrastructure(this IServiceCollection services, ConfigurationManager config)
    {
        services.AddScoped<IParkedCarRepository, ParkedCarRepository>();
        services.Configure<EmailOptions>(config.GetSection("EMAIL_CONFIG"));
        services.AddTransient<IEmailSender<ApplicationUser>, EmailSender>();
    }
}
