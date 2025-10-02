using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VitalScope.Insfrastructure.Models;

namespace VitalScope.Insfrastructure.EntityTypeConfigurations;

public sealed class PatientEntityConfiguration : IEntityTypeConfigurationApplication<PatientEntity>
{
    public void Configure(EntityTypeBuilder<PatientEntity> builder)
    {
        builder.ToTable("Patients", "vital");
    }
}