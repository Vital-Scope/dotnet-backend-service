using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VitalScope.Insfrastructure.Models;

namespace VitalScope.Insfrastructure.EntityTypeConfigurations;

public sealed class UserSettingEntityConfiguration : IEntityTypeConfigurationApplication<UserSettingEntity>
{
    public void Configure(EntityTypeBuilder<UserSettingEntity> builder)
    {
        builder.ToTable("UserSettings", "vital");
        
        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.User)
            .WithOne(c => c.UserSettings)
            .OnDelete(DeleteBehavior.Restrict);
    }
}