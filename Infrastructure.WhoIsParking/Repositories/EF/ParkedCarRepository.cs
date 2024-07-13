using App.WhoIsParking.Interfaces.Repositories;
using Domain.WhoIsParking.Models;
using Infrastructure.WhoIsParking.Data.EntitiesConfig;

namespace Infrastructure.WhoIsParking.Repositories.EF;

internal class ParkedCarRepository : IParkedCarRepository
{
    private readonly DataContext _dbContext;

    public ParkedCarRepository(DataContext dataContext)
    {
        _dbContext = dataContext;
    }

    public Task<ParkedCar?> GetAggregateAsync(int id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public Task<ParkedCar?> GetAsync(int id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public async Task<ParkedCar> AddAsync(ParkedCar entity, CancellationToken token)
    {
        var entry = await _dbContext.AddAsync(entity, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return entry.Entity;
    }

    public Task DeleteAsync(ParkedCar entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }


    public Task<ParkedCar> UpdateAsync(ParkedCar entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
