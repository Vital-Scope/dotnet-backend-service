using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace VitalScope.Insfrastructure.Specifications.Base;

public interface ISpecification<TEntity>
    where TEntity : class
{
    int Count { get; }

    int Offset { get; }

    bool Trackable { get; }

    Expression<Func<TEntity, bool>> Criteria { get; }

    List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> Orderes { get; }

    List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }
}