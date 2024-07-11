using System.Security.Cryptography;

namespace App.WhoIsParking.Interfaces.Repositories;

public interface IRepository<T, TID>
{
    // Query
    Task<T?> GetAsync(TID id, CancellationToken token);
    Task<T?> GetAggregateAsync(TID id, CancellationToken token);

    // Command
    Task<T> AddAsync(T entity, CancellationToken token);
    Task<T> UpdateAsync(T entity, CancellationToken token);
    Task DeleteAsync(T entity, CancellationToken token);
}
