using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConsoleAsyncLocal.Services.Options;

namespace ConsoleAsyncLocal.Services.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>AddAsync services to the service collection.</summary>
    /// <remarks>
    /// Note: Microsoft.Extensions.Options.ConfigurationExtensions NuGet package is required to use IServiceCollection
    /// </remarks>
    public static IServiceCollection AddExampleServices(this IServiceCollection services, IConfiguration config)
    {
        // Options pattern: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#the-options-pattern
        // If you need to register it with DI, use DI services to configure options: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#use-di-services-to-configure-options
        services.Configure<ServiceOptions>(config.GetSection(ServiceOptions.SectionName));


        // Register services for this library
        services.AddTransient<IWorkerService, WorkerService>();
        services.AddScoped<IDataContextService, DataContextService>();

        return services;
    }
}