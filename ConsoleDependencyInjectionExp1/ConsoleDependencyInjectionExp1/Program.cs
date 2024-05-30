using System.Reflection;
using ConsoleDependencyInjectionExp1.Services;
using ConsoleDependencyInjectionExp1.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
 
var collection = new ServiceCollection();

AddMyDependencies(collection, GetConfiguration());

var serviceProvider = collection.BuildServiceProvider();

await TestTheMathServiceAsync(serviceProvider);



// Create a configuration object from appsettings.json and user secrets
static IConfiguration GetConfiguration()
{
    // IConfiguration requires: Microsoft.Extensions.Configuration NuGet package
    // AddJsonFile requires:    Microsoft.Extensions.Configuration.Json NuGet package
    // https://stackoverflow.com/a/46437144/97803
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(Assembly.GetExecutingAssembly());

    return builder.Build();
}

// Register your dependencies here
static void AddMyDependencies(IServiceCollection serviceCollection, IConfiguration config)
{
    // Since we create it, we need to register it
    serviceCollection.AddSingleton(config);
    
    // Register YOUR dependencies here
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