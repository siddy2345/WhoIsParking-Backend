using App.WhoIsParking.Interfaces.Repositories;
using Infrastructure.WhoIsParking.Data.EntitiesConfig;

namespace Infrastructure.WhoIsParking.Repositories.EF;

internal class BaseRepository<TEntity, TId> : IRepository<TEntity, TId>
    where TEntity : class where TId : IEquatable<TId>
{
    private readonly DataContext _dbContext;

    public BaseRepository(DataContext dbContext)
    {
        _dbContext = dbContext;
    }

    public virtual async Task<TEntity> AddAsync(TEntity entity, CancellationToken token)
    {
        var entry = await _dbContext.AddAsync(entity, token).ConfigureAwait(false);
        await _dbContext.SaveChangesAsync(token).ConfigureAwait(false);
        return entry.Entity;
    }

    public virtual Task DeleteAsync(TEntity entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TEntity?> GetAggregateAsync(TId id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TEntity?> GetAsync(TId id, CancellationToken token)
    {
        throw new NotImplementedException();
    }

    public virtual Task<TEntity> UpdateAsync(TEntity entity, CancellationToken token)
    {
        throw new NotImplementedException();
    }
}
