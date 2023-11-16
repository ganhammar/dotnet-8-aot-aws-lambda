using System.Diagnostics.CodeAnalysis;
using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

namespace GetFunction;

public class Function
{
  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(Function))]
  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyRequest))]
  [DynamicDependency(DynamicallyAccessedMemberTypes.All, typeof(APIGatewayHttpApiV2ProxyResponse))]
  static Function()
  {
    AWSSDKHandler.RegisterXRayForAllServices();
  }

  private static async Task Main()
  {
    Func<APIGatewayHttpApiV2ProxyRequest, ILambdaContext, APIGatewayHttpApiV2ProxyResponse> handler = FunctionHandler;
    await LambdaBootstrapBuilder
      .Create(handler, new SourceGeneratorLambdaJsonSerializer<CustomJsonSerializerContext>(options =>
      {
        options.PropertyNameCaseInsensitive = true;
      }))
      .Build()
      .RunAsync();
  }

  public static APIGatewayHttpApiV2ProxyResponse FunctionHandler(
      APIGatewayHttpApiV2ProxyRequest apiGatewayHttpApiV2ProxyRequest, ILambdaContext context)
  {
    if (!apiGatewayHttpApiV2ProxyRequest.QueryStringParameters.TryGetValue("who", out var who))
    {
      return new APIGatewayHttpApiV2ProxyResponse
      {
        Body = "Who are you? 🤔",
        StatusCode = (int)HttpStatusCode.BadRequest,
      };
    }

    return new APIGatewayHttpApiV2ProxyResponse
    {
      StatusCode = (int)HttpStatusCode.OK,
      Body = $"Hello {who} 👋"
    };
  }
}