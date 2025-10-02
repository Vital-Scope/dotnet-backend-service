using VitalScope.Insfrastructure.Common;

namespace VitalScope.Insfrastructure.Models;

public sealed class PatientEntity : BaseEntity
{
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public DateTime? BirthDate { get; set; }
    
    public Guid? ClientId { get; set; }
    
    public int? PregnancyWeek { get; set; }
    
    public int? PregnancyNumber { get; set; }
    
    public string? Anamnesis { get; set; }
    
    public string? DoctorNotes { get; set; }
    
    public string? Avatar { get; set; }
    
    
    public bool IsDeleted { get; set; }

    public ICollection<StudyMetaInformationEntity>  StudyMetaInformations { get; set; }
}