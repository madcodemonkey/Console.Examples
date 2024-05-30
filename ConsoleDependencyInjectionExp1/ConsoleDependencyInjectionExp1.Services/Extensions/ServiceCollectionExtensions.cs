using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ConsoleDependencyInjectionExp1.Services.Options;

namespace ConsoleDependencyInjectionExp1.Services.Extensions;

public static class ServiceCollectionExtensions
{
    /// <summary>Add services to the service collection.</summary>
    /// <remarks>
    /// Note: Microsoft.Extensions.Options.ConfigurationExtensions NuGet package is required to use IServiceCollection
    /// </remarks>
    public static IServiceCollection AddExampleServices(this IServiceCollection services, IConfiguration config)
    {
        // Options pattern: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#the-options-pattern
        // If you need to register it with DI, use DI services to configure options: https://learn.microsoft.com/en-us/aspnet/core/fundamentals/configuration/options?view=aspnetcore-8.0#use-di-services-to-configure-options
        services.Configure<ServiceOptions>(config.GetSection(ServiceOptions.SectionName));


        // Register services for this library
        services.AddTransient<IMathService, MathService>();

        return services;
    }
}