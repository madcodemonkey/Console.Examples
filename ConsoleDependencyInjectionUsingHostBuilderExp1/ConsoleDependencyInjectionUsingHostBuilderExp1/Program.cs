using ConsoleDependencyInjectionUsingHostBuilderExp1.Services;
using ConsoleDependencyInjectionUsingHostBuilderExp1.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Reflection;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);
builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly()); // Needed if you want secrets.json to be loaded

AddMyDependencies(builder.Services, builder.Configuration);

using IHost host = builder.Build();

await TestTheMathServiceAsync(host.Services, args);

// Note 1: We never call host.RunAsync() because we don't want to stop it using CTRL-C.  We just want it to run our service once and then stop.
// Note 2: Not call host.Run or host.RunAsync is a legitimate way of shutting down.  It is listed here as one of three ways to stop the host:
//         https://learn.microsoft.com/en-us/dotnet/core/extensions/generic-host?tabs=appbuilder#host-shutdown
// Note 3: Very similar to this example: https://stackoverflow.com/a/58277690/97803
await host.StopAsync();


// Register your dependencies here
static void AddMyDependencies(IServiceCollection serviceCollection, IConfiguration config)
{
    serviceCollection.AddExampleServices(config);
}

// Test the MathService
static async Task TestTheMathServiceAsync(IServiceProvider serviceProvider, string[] args)
{
    if (args.Length < 1 || !int.TryParse(args[0], out var firstNumber))
    {
        firstNumber = 1;
    }

    if (args.Length < 2 || !int.TryParse(args[1], out var secondNumber))
    {
        secondNumber = 2;
    }

    using IServiceScope serviceScope = serviceProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;

    var mathService = provider.GetRequiredService<IMathService>();
    var result = await mathService.AddAsync(firstNumber, secondNumber);

    Console.WriteLine($"{firstNumber} + {secondNumber} = {result}");
}