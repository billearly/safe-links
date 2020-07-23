# SafeLinks API

ASP.NET Core Web Api Serverless Application

This is a Web API built with .NET Core 3.1 and deployed to AWS Lambda. This serves as the backend for the [SafeLinks Site](https://github.com/billearly/safe-links-site).

## Retrospective

The primary goal of this project was to build an application that I would actually use. I often find myself suspicious of shortened links and want to know where I'm going to end up. Each shortener service has its own way to figure out where a link will go, but I wanted a more universal solution. I also wanted to be able to do this on any general link I come across.

I decided to use .NET Core because even though I have a lot of professional .NET experience I didn't have any personal projects using this framework. Also, professionaly I'm still mainly working with .NET Framework 4.X applications and this was a chance to get more experience with Core.

If I had more time, some things I would like to add:
* Automatically make additional requests to follow a full redirect chain
* Disregard 'common' redirects like http -> https or lowercasing
* Persist the known shortener service information elsewhere. A simple keyvalue store

If I could do it over again, some things I would change:
* Reconsider using the ExceptionFilter as an intentional error handler

---

### Notes: ###

* LocalEntryPoint.cs - For local development
* LambdaEntryPoint.cs - Class that derives from **Amazon.Lambda.AspNetCoreServer.APIGatewayProxyFunction**. The Lambda function (`FunctionHandlerAsync`) is defined in the base class and is supplied as the handler.
* **aws-lambda-tools-defaults.json** and **serverless.template** have been removed as right now this is being manually deployed to AWS
* Release: `dotnet publish -c:Release`