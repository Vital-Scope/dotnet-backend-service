using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VitalScope.Insfrastructure.Models;

namespace VitalScope.Insfrastructure.EntityTypeConfigurations;

public sealed class UserEntityConfiguration : IEntityTypeConfigurationApplication<UserEntity>
{
    public void Configure(EntityTypeBuilder<UserEntity> builder)
    {
        builder.ToTable("Users", "vital");
        
        builder.HasKey(x => x.Id);
    }
}