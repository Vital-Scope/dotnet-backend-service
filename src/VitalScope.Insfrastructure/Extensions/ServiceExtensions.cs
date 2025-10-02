using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VitalScope.Common.Consts;
using VitalScope.Insfrastructure.Interceptors;
using VitalScope.Insfrastructure.Repositories;
using VitalScope.Insfrastructure.Repositories.Base;

namespace VitalScope.Insfrastructure.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services)
    {
        services.AddScoped(typeof(IRepository<>), typeof(DefaultRepository<>));
       // services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        
        return services;
    }
    
    public static IServiceCollection AddDatabase<TContext>(this IServiceCollection services,
        string migrationsHistoryTableName) where TContext : DbContext
    {
        return services.AddDbContext<TContext>((provider, builder) =>
        {
            var connectionString = provider.GetRequiredService<IConfiguration>()
                .GetConnectionString(CommonConsts.ConnectionVitalScope);

          //  builder.AddInterceptors(provider.GetRequiredService<ISaveChangesInterceptor>());
            
            builder.UseNpgsql(connectionString,
                b =>
                {
                    b.MigrationsAssembly(typeof(ServiceExtensions).Assembly.GetName().Name);
                    b.CommandTimeout(180);
                    b.MigrationsHistoryTable(migrationsHistoryTableName);
                }).EnableSensitiveDataLogging();
        });
    }
}