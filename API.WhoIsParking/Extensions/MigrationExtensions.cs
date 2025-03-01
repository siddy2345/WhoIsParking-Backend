using Infrastructure.WhoIsParking.Data.EntitiesConfig;
using Microsoft.EntityFrameworkCore;

namespace API.WhoIsParking.Extensions;

public static class MigrationExtensions
{
    public static void ApplyMigrations(this IApplicationBuilder app)
    {
        using IServiceScope scope = app.ApplicationServices.CreateScope();

        using DataContext dataContext = scope.ServiceProvider.GetRequiredService<DataContext>();

        dataContext.Database.Migrate();
    }
}
