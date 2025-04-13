using App.WhoIsParking.Interfaces.Repositories;
using App.WhoIsParking.UseCases.Houses.Queries.GetAll;
using Domain.WhoIsParking.Models;
using Infrastructure.WhoIsParking.Data.EntitiesConfig;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.WhoIsParking.Repositories.EF;

internal class HouseRepository : BaseRepository<House, int>, IHouseRepository
{
    private readonly DataContext _dbContext;

    public HouseRepository(DataContext dbContext) : base(dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IReadOnlyCollection<HouseReadAllResult>> ReadHousesByTenant(Guid tenantId, CancellationToken token)
    {
        var query = from h in _dbContext.House
                    where h.TenantId == tenantId

                    select new HouseReadAllResult()
                    {
                        HouseId = h.HouseId,
                        Number = h.Number,
                        Street = h.Street,
                    };

        return await query.AsNoTracking().ToListAsync(token).ConfigureAwait(false);
    }

    public Task<Guid> ReadTenantIdByHouseId(int houseId, CancellationToken token)
    {
        return _dbContext.House
            .Where(house => house.HouseId == houseId)
            .Select(house => house.TenantId)
            .SingleOrDefaultAsync(token);
    }
}
