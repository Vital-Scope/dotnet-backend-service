namespace VitalScope.Common.Options;

public sealed class JwtOptions
{
    public string ValidAudience { get; set; }
    public string ValidIssuer { get; set; }
    public string Secret { get; set; }

    public int ExpiresHour { get; set; }
}