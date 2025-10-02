using System.Text.Json.Serialization;

namespace VitalScope.Logic.Models.Business;

public class Comments
{
    [JsonPropertyName("outcome_measures")]
    public OutcomeMeasures? OutcomeMeasures { get; set; }

    [JsonPropertyName("maternal_factors")]
    public MaternalFactors? MaternalFactors { get; set; }

    [JsonPropertyName("delivery")]
    public Delivery? Delivery { get; set; }
}