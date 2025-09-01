namespace VitalScope.Logic.Models.Output;

public class StudyModel
{
    public MetaInformatio MetaInformatio { get; set; }
    
    public IEnumerable<Maininformation> Values { get; set; } 
}