using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query;

namespace VitalScope.Insfrastructure.Specifications.Base;

public abstract class BaseSpecification<TEntity> : ISpecification<TEntity>
    where TEntity : class
{
    protected BaseSpecification(Expression<Func<TEntity, bool>> criteria)
    {
        Criteria = criteria;

        Orderes = new List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>>();
        Includes = new List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>>();
    }

    public int Count { get; set; } = 10;

    public int Offset { get; set; } = 0;

    public bool Trackable { get; set; } = false;

    public Expression<Func<TEntity, bool>> Criteria { get; }

    public List<Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>>> Orderes { get; }

    public List<Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>>> Includes { get; }

    public void AddOrder(Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orders) => Orderes.Add(orders);

    public void AddInclude(Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include) => Includes.Add(include);
}