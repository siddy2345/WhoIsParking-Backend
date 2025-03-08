namespace App.WhoIsParking.Interfaces.Repositories;

public interface IRepository<TEntity, TId> where TEntity: class where TId : IEquatable<TId>
{
    // Query
    Task<TEntity?> GetAsync(TId id, CancellationToken token);
    Task<TEntity?> GetAggregateAsync(TId id, CancellationToken token);

    // Command
    Task<TEntity> AddAsync(TEntity entity, CancellationToken token);
    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token);
    Task DeleteAsync(TEntity entity, CancellationToken token);
}
