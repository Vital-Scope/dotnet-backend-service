namespace VitalScope.Logic.Models.Output.User;

public sealed class UserOutputModel
{
    public string Token { get; init; }

    public DateTime Expiration { get; init; }
    
    public string? FirstName { get; init; }
    
    public string? LastName { get; init; }
    
    public Guid Id { get; init; }

    public UserOutputModel(string token, DateTime expiration, string? firstName, string? lastName, Guid id)
    {
        Token = token;
        Expiration = expiration;
        FirstName = firstName;
        LastName = lastName;
        Id = id;
    }
}