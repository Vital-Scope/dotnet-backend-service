using VitalScope.Common.Enums;
using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public sealed class StudyMainInformationEntity : BaseEntity
{
    public double Time { get; set; }
    
    public ChannelType Channel { get; set; }  // FHR / UC
    
    public double Value { get; set; }
    
    public StudyMetaInformationEntity StudyMetaInformation { get; set; }

    public Guid StudyMetaInformationId { get; set; }
}