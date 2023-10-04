using System.Net;
using Amazon.Lambda.APIGatewayEvents;
using Amazon.Lambda.Core;
using Amazon.Lambda.RuntimeSupport;
using Amazon.Lambda.Serialization.SystemTextJson;
using Amazon.XRay.Recorder.Handlers.AwsSdk;

public class Function
{
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

    public static APIGatewayHttpApiV2ProxyResponse FunctionHandler(APIGatewayHttpApiV2ProxyRequest apigProxyEvent, ILambdaContext context)
    {
        return new APIGatewayHttpApiV2ProxyResponse
        {
            StatusCode = (int)HttpStatusCode.OK,
            Body = $"Hello 👋"
        };
    }
}