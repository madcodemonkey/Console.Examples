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

await TestTheMathServiceAsync(host.Services);

// Note:  we never call host.StartAsync() because we don't want to stop it using CTRL-C.  We just want it to run our service once and then stop.
await host.StopAsync();


// Register your dependencies here
static void AddMyDependencies(IServiceCollection serviceCollection, IConfiguration config)
{
    serviceCollection.AddExampleServices(config);
}

// Test the MathService
static async Task TestTheMathServiceAsync(IServiceProvider hostProvider)
{
    using IServiceScope serviceScope = hostProvider.CreateScope();
    IServiceProvider provider = serviceScope.ServiceProvider;
    
    var mathService = provider.GetRequiredService<IMathService>();
    var result = await mathService.AddAsync(1, 2);

    Console.WriteLine($"1 + 2 = {result}");
}