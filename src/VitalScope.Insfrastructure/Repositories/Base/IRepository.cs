using VitalScope.Common.Options;
using VitalScope.Insfrastructure.Specifications.Base;

namespace VitalScope.Insfrastructure.Repositories.Base;

public interface IRepository<TEntity>
    where TEntity : class
{
    RepositoryOptions Options { get; }

    Task SaveChangesAsync(CancellationToken cancellationToken = default);

    Task<TEntity> AddAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task AddRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity> RemoveAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task RemoveRangeAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);

    Task<int> CountAsync(ISpecification<TEntity> specification = default, CancellationToken cancellationToken = default);

    Task<TEntity> GetFirstOrDefaultAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetListAsync(ISpecification<TEntity> specification, CancellationToken cancellationToken = default);
}