using Domain.WhoIsParking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.WhoIsParking.Data.EntitiesConfig;

public class DataContext : IdentityDbContext
{
    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    public DbSet<House> House { get; set; }

    public DbSet<ParkedCar> ParkedCar { get; set; }
}
