# Minimum dotnet 8 native AOT example using CDK

This project is a minimalistic example of how to build, publish, and deploy dotnet 8 AOT (ahead-of-time compilation) projects to AWS lambda using CDK. It contains two functions, one that is expected to be called through HTTP POST, and one through HTTP GET. The CDK stack also contains an HTTP rest API through which the functions can be invoked.