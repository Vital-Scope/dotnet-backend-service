namespace VitalScope.Logic.Models.Output.Authentication;

public class RegisterResult
{
    public  Guid? Id { get; set; }

    public bool IsSucceeded { get; set; }

    public IEnumerable<string> Errors { get; set; }
}