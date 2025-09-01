using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VitalScope.Insfrastructure.Models;

namespace VitalScope.Insfrastructure.EntityTypeConfigurations;

public sealed class StudyMetaEntityConfiguration : IEntityTypeConfigurationApplication<StudyMetaInformationEntity>
{
    public void Configure(EntityTypeBuilder<StudyMetaInformationEntity> builder)
    {
        builder.ToTable("StudyMetas", "vital");
        
        builder.HasOne(x => x.Patient)
            .WithMany(x => x.StudyMetaInformations)
            .HasForeignKey(x => x.PatientId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}