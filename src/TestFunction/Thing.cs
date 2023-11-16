using System.Text.Json.Serialization;

namespace TestFunction;

public class Thing
{
  [JsonPropertyName("who")]
  public string? Who { get; set; }
}