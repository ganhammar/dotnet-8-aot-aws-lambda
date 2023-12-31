﻿using Amazon.CDK;
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

    // Post
    var post = new Function(this, "PostFunction", new FunctionProps
    {
      Runtime = Runtime.PROVIDED_AL2023,
      Architecture = Architecture.X86_64,
      Handler = "PostFunction::PostFunction.Function::FunctionHandler",
      Code = Code.FromAsset($"./.output/PostFunction.zip"),
      Timeout = Duration.Minutes(1),
      MemorySize = 128,
      LogRetention = RetentionDays.ONE_DAY,
    });
    api.Root.AddMethod("POST", new LambdaIntegration(post));

    // Get
    var get = new Function(this, "GetFunction", new FunctionProps
    {
      Runtime = Runtime.PROVIDED_AL2023,
      Architecture = Architecture.X86_64,
      Handler = "GetFunction::GetFunction.Function::FunctionHandler",
      Code = Code.FromAsset($"./.output/GetFunction.zip"),
      Timeout = Duration.Minutes(1),
      MemorySize = 128,
      LogRetention = RetentionDays.ONE_DAY,
    });
    api.Root.AddMethod("GET", new LambdaIntegration(get));

    // Output
    _ = new CfnOutput(this, "APIGWEndpoint", new CfnOutputProps
    {
      Value = api.Url,
    });
  }
}
