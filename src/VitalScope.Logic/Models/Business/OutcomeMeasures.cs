using System.Text.Json.Serialization;

namespace VitalScope.Logic.Models.Business;

public class OutcomeMeasures
{
    [JsonPropertyName("pH")]
    public double? PH { get; set; }

    [JsonPropertyName("BDecf")]
    public double? BDecf { get; set; }

    [JsonPropertyName("pCO2")]
    public double? PCO2 { get; set; }

    [JsonPropertyName("BE")]
    public double? BE { get; set; }

    [JsonPropertyName("Apgar1")]
    public int? Apgar1 { get; set; }

    [JsonPropertyName("Apgar5")]
    public int? Apgar5 { get; set; }
}