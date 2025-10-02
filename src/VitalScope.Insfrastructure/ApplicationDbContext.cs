using System.Reflection;
using Microsoft.EntityFrameworkCore;

namespace VitalScope.Insfrastructure;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly(), type =>
        {
            return type.GetInterfaces()
                .Any(x => x.IsGenericType &&
                          x.GetGenericTypeDefinition() == typeof(IEntityTypeConfigurationApplication<>));
        });
    }
}