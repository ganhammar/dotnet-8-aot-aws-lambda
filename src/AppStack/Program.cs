using Amazon.CDK;

namespace AppStack;

public class Program
{
  static void Main(string[] args)
  {
    var app = new App(null);

    _ = new AppStack(app, "dotnet-8-aot-stack", new StackProps
    {
      Env = new Amazon.CDK.Environment
      {
        Region = "eu-north-1",
      },
    });

    app.Synth();
  }
}
