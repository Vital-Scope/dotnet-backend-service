namespace VitalScope.Logic.Models.Input.Patient;

public sealed class EditPatientModel
{
    public Guid Id { get; set; }
    
    public string? FirstName { get; set; }
    
    public string? LastName { get; set; }
    
    public string? MiddleName { get; set; }
    
    public long? BirthDate { get; set; }
    
    public Guid? ClientId { get; set; }
    
    public int? PregnancyWeek { get; set; }
    
    public int? PregnancyNumber { get; set; }
    
    public string? Anamnesis { get; set; }
    
    public string? DoctorNotes { get; set; }
    
    public string? Avatar { get; set; }
}