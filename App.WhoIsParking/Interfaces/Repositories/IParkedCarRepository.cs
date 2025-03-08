using App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;
using Domain.WhoIsParking.Models;

namespace App.WhoIsParking.Interfaces.Repositories;

public interface IParkedCarRepository : IRepository<ParkedCar, int>
{
    Task<IReadOnlyCollection<ParkedCarReadAllResult>> ReadParkedCars(GetAllParkedCarsCommand request, CancellationToken token);
}
