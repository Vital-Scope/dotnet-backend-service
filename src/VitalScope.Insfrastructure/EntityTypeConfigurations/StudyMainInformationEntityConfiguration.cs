using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VitalScope.Insfrastructure.Models;

namespace VitalScope.Insfrastructure.EntityTypeConfigurations;

public sealed class StudyMainInformationEntityConfiguration : IEntityTypeConfigurationApplication<StudyMainInformationEntity>
{
    public void Configure(EntityTypeBuilder<StudyMainInformationEntity> builder)
    {
        builder.ToTable("StudyMain", "vital");
        
        builder.HasOne(x => x.StudyMetaInformation)
            .WithMany(x => x.InfoMetas)
            .HasForeignKey(x => x.StudyMetaInformationId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}