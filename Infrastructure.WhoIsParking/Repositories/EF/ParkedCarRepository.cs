using App.WhoIsParking.Interfaces.Repositories;
using App.WhoIsParking.UseCases.ParkedCars.Queries.GetAll;
using Domain.WhoIsParking.Models;
using Infrastructure.WhoIsParking.Data.EntitiesConfig;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.WhoIsParking.Repositories.EF;

internal class ParkedCarRepository : BaseRepository<ParkedCar, int>, IParkedCarRepository
{
    private readonly DataContext _dbContext;

    public ParkedCarRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<ParkedCarReadAllResult>> ReadParkedCars(GetAllParkedCarsCommand request, CancellationToken token)
    {
        var query = from pc in _dbContext.ParkedCar
                    join h in _dbContext.House
                        on pc.HouseId equals h.HouseId

                    where pc.TenantId == request.TenantId
                    && h.TenantId == request.TenantId
                    && request.DateFrom <= DateOnly.FromDateTime(pc.Arrival) 
                    && request.DateTo >= DateOnly.FromDateTime(pc.Arrival)
                    && request.HouseIds.Contains(h.HouseId)

                    select new ParkedCarReadAllResult
                    {
                        ParkedCarId = pc.ParkedCarId,
                        CarBrand = pc.CarBrand,
                        Arrival = TimeZoneInfo.ConvertTimeFromUtc(pc.Arrival, TimeZoneInfo.FindSystemTimeZoneById(pc.TimeZoneInfo)),
                        Firstname = pc.Firstname,
                        Lastname = pc.Lastname,
                        NumberPlate = pc.NumberPlate,
                        HouseAdress = h.Street,
                        HouseNumber = h.Number,
                        Zip = h.Zip
                    };

        return await query.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }
}
