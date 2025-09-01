using VitalScope.Insfrastructure;
using VitalScope.Insfrastructure.Repositories.Base;

namespace VitalScope.Insfrastructure.Repositories;

internal sealed class DefaultRepository<TEntity> : BaseRepository<ApplicationDbContext, TEntity>, IRepository<TEntity>
    where TEntity : class
{
    public DefaultRepository(ApplicationDbContext context)
        : base(context)
    {
    }
}