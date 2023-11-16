using System.Text.Json.Serialization;
using Amazon.Lambda.APIGatewayEvents;

namespace PostFunction;

[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyRequest))]
[JsonSerializable(typeof(APIGatewayHttpApiV2ProxyResponse))]
[JsonSerializable(typeof(List<string>))]
[JsonSerializable(typeof(Dictionary<string, string>))]
[JsonSerializable(typeof(Thing))]
public partial class CustomJsonSerializerContext : JsonSerializerContext
{
}