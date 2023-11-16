using System.Text.Json.Serialization;

namespace GetFunction;

public class Thing
{
  [JsonPropertyName("who")]
  public string? Who { get; set; }
}