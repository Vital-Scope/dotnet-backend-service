namespace VitalScope.Logic.Models.Output.Authentication;

public class LoginResult
{
    public string Token { get; set; }

    public DateTime Expiration { get; set; }
    
    public Guid Id { get; set; }
}