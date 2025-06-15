using App.WhoIsParking.UseCases.Houses.Queries.Common;
using Domain.WhoIsParking.Models;

namespace App.WhoIsParking.Interfaces.Repositories;

public interface IHouseRepository : IRepository<House, int>
{
    Task<IReadOnlyCollection<HouseReadAllResult>> ReadHousesByTenant(Guid tenantId, CancellationToken token);
    Task<Guid> ReadTenantIdByHouseId(int houseId, CancellationToken token);
    Task<HouseReadResult?> ReadHouseById(int houseId, Guid tenantId, CancellationToken token);
}
