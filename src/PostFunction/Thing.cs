using System.Text.Json.Serialization;

namespace PostFunction;

public class Thing
{
  [JsonPropertyName("who")]
  public string? Who { get; set; }
}