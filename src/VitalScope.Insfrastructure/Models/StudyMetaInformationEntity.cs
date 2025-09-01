using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public sealed class StudyMetaInformationEntity : BaseEntity
{
    public string? Name { get; set; }
    
    public int? NumSignals { get; set; }
    
    //public IEnumerable<string>? SignalNames { get; set; }
    
    public int? SamplingFrequency { get; set; }
    
   // public IEnumerable<string>? SigUnits { get; set; }
    
    public DateTime? Date { get; set; }
    
    public float? Ph { get; set; }
    
    public float? BDecf { get; set; }
    
    public float? PCO2 { get; set; }
    
    public float? BE { get; set; }
    
    public float? Apgar1 { get; set; }
    
    public float? Apgar5 { get; set; }
    
    public int? Age { get; set; }
    
    public int? Gravidity { get; set; }
    
    public int? Parity { get; set; }
    
    public int? Diabetes {get; set; }
    
    public int? Presentation { get; set; }
    
    public int? Istage { get; set; }
    
    public int? IIstage { get; set; }
    
    public int? Delivtype { get; set; }
    
    public ICollection<StudyMainInformationEntity>? InfoMetas { get; set; }
    
    public PatientEntity? Patient { get; set; }
    
    public Guid? PatientId { get; set; }
}