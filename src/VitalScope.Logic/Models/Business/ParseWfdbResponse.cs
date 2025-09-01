using System.Text.Json.Serialization;

namespace VitalScope.Logic.Models.Business;

public class ParseWfdbResponse
{
  //  [JsonPropertyName("metadata")]
    public Metadata? Metadata { get; set; }

   // [JsonPropertyName("data")]
    public List<DataPoint>? Data { get; set; }
}