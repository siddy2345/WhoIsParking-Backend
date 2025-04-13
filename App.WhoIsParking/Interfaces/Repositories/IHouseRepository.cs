using App.WhoIsParking.UseCases.Houses.Queries.GetAll;
using Domain.WhoIsParking.Models;

namespace App.WhoIsParking.Interfaces.Repositories;

public interface IHouseRepository : IRepository<House, int>
{
    Task<IReadOnlyCollection<HouseReadAllResult>> ReadHousesByTenant(Guid tenantId, CancellationToken token);
    Task<Guid> ReadTenantIdByHouseId(int houseId, CancellationToken token);
}
