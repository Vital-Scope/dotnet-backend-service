using System.Text.Json.Serialization;

namespace VitalScope.Logic.Models.Business;

public class Metadata
{
    [JsonPropertyName("record_name")]
    public string? RecordName { get; set; }

    [JsonPropertyName("num_signals")]
    public int? NumSignals { get; set; }

    [JsonPropertyName("signal_names")]
    public List<string>? SignalNames { get; set; }

    [JsonPropertyName("sampling_frequency")]
    public int? SamplingFrequency { get; set; }

    [JsonPropertyName("sig_units")]
    public List<string>? SigUnits { get; set; }

    [JsonPropertyName("base_date")]
    public DateTime? BaseDate { get; set; }

    [JsonPropertyName("base_time")]
    public TimeSpan? BaseTime { get; set; }

    [JsonPropertyName("comments")]
    public Comments? Comments { get; set; }
}