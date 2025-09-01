using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace VitalScope.Insfrastructure.Extensions;

public static class DatabaseExtension
{
    public static async Task ApplyMigrationAsync<TContext>(this IServiceProvider provider, CancellationToken cancellationToken = default)
        where TContext : DbContext
    {
        using var scope = provider.CreateScope();

        await using var context = scope.ServiceProvider
            .GetRequiredService<TContext>();

        await context.Database.MigrateAsync(cancellationToken);
    }
}