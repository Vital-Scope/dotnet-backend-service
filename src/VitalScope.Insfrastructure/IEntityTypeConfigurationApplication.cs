using Microsoft.EntityFrameworkCore;

namespace VitalScope.Insfrastructure;

public interface IEntityTypeConfigurationApplication<TEntity> : 
    IEntityTypeConfiguration<TEntity> where TEntity : class;