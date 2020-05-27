# SafeLinks API

ASP.NET Core Web Api Serverless Application

This is a Web API built with .NET Core 3.1 and deployed to AWS Lambda. This serves as the backend for the SafeLinks Site.

## Getting started:

To start the app, which can then be accessed on https://localhost:5001

```
    cd src
    dotnet run
```

To run tests
```
    cd test
    dotnet test
```

### Notes: ###

* LocalEntryPoint.cs - For local development
* LambdaEntryPoint.cs - Class that derives from **Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction**. The Lambda function (`FunctionHandlerAsync`) is defined in the base class and is supplied as the handler.