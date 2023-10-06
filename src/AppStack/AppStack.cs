using Amazon.CDK;
using Amazon.CDK.AWS.APIGateway;
using Amazon.CDK.AWS.Lambda;
using Amazon.CDK.AWS.Logs;
using Constructs;

namespace AppStack;

public class AppStack : Stack
{
  internal AppStack(Construct scope, string id, IStackProps props)
    : base(scope, id, props)
  {

    // API Gateway
    var api = new RestApi(this, "dotnet-8-aot-api", new RestApiProps
    {
      RestApiName = "dotnet-8-aot-api",
      DefaultCorsPreflightOptions = new CorsOptions
      {
        AllowOrigins =
        [
          "http://localhost:3000",
        ],
      },
    });

    // Create
    var function = new Function(this, "TestFunction", new FunctionProps
    {
      Runtime = Runtime.PROVIDED_AL2,
      Architecture = Architecture.X86_64,
      Handler = "TestFunction::TestFunction.Function::FunctionHandler",
      Code = Code.FromAsset($"./.output/TestFunction.zip"),
      Timeout = Duration.Minutes(1),
      MemorySize = 256,
      LogRetention = RetentionDays.ONE_DAY,
    });
    api.Root.AddMethod("GET", new LambdaIntegration(function));

    // Output
    new CfnOutput(this, "APIGWEndpoint", new CfnOutputProps
    {
      Value = api.Url,
    });
  }
}
