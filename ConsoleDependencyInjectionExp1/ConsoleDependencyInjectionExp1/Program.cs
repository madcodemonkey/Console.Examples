using System.Reflection;
using ConsoleDependencyInjectionExp1.Services;
using ConsoleDependencyInjectionExp1.Services.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

// Setup DI
var collection = new ServiceCollection();
AddMyDependencies(collection);
var serviceProvider = collection.BuildServiceProvider();

// Get ONE service as a starting point. Everything else should be resolved by DI.
var menuRepository = serviceProvider.GetService<IMathService>();

var result = menuRepository.Add(1, 2);

Console.WriteLine($"1 + 2 = {result}");

static void AddMyDependencies(IServiceCollection serviceCollection)
{

    // IConfiguration requires: Microsoft.Extensions.Configuration NuGet package
    // AddJsonFile requires:    Microsoft.Extensions.Configuration.Json NuGet package
    // https://stackoverflow.com/a/46437144/97803
    var builder = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json")
        .AddUserSecrets(Assembly.GetExecutingAssembly());

    IConfiguration config = builder.Build();
    
    serviceCollection.AddSingleton<IConfiguration>(config);
    
    // Register YOUR dependencies here
    serviceCollection.AddExampleServices(config);
}