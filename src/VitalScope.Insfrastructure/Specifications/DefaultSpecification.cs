using System.Linq.Expressions;

namespace VitalScope.Insfrastructure.Specifications;

public static class DefaultSpecification
{
    public static Expression<Func<TEntity, bool>> EmptyCriteria<TEntity>()
    {
        return default;
    }
}