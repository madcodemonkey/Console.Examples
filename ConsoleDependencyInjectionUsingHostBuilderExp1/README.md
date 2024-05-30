# Console Dependency Injection Example 1

## Summary
This is a example using the host builder. 

It is based off of [this example](https://learn.microsoft.com/en-us/dotnet/core/extensions/dependency-injection-usage) from Micrsoft; 
however, I've modified to NOT call host.RunAsync() so that it will shutdown after running; otherwise, you would have hit CTRL-C to stop it.

## NuGet Packages needed
- Microsoft.Extensions.Hosting
   - Needed to use the builders for the host
   - You'll get Microsoft.Extensions.Configuration automatically
   - You'll get Microsoft.Extensions.Configuration.Json automatically
   - You'll get Microsoft.Extensions.DependencyInjection automatically
 