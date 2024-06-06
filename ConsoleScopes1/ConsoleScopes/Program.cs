using ConsoleScopes.Services;
using ConsoleScopes.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Xml.Linq;


class Program
{
    static async Task<int> Main(string[] args)
    {
        var builder = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                services.AddHttpClient();
                services.AddExampleServices(hostContext.Configuration);
            }).UseConsoleLifetime();

        var host = builder.Build();

        try
        {
            // Assign values
            var mySingletonService1 = host.Services.GetRequiredService<IMySingletonService>();
            mySingletonService1.Number = 42;
            var dataContextService1 = host.Services.GetRequiredService<IDataContextService>();
            dataContextService1.UserName = "Patrick";

            // Log what happens before
            Console.WriteLine($"MAIN Root Scope Before async call - Name: {dataContextService1.UserName} Number: {mySingletonService1.Number}");

            // All Scoped
            //var work1 = WorkUsesTheScopeItCreateAsync(host, "John", 44);
            //var work2 = WorkUsesTheScopeItCreateAsync(host, "Lynn", 56);
            //var work3 = WorkUsesTheScopeItCreateAsync(host, "Zack", 87);

            // Mixture
            var work1 = WorkUsesTheScopeItCreateAsync(host, "John", 44);
            var work2 = WorkUsesRootScopeAsync(host, "Lynn", 56);
            var work3 = WorkUsesRootScopeAsync(host, "Zack", 87);

            Task.WaitAll(new[] { work2, work1, work3 });

            // Log what happens afterwards
            var dataContextService2 = host.Services.GetRequiredService<IDataContextService>();
            var mySingletonService2 = host.Services.GetRequiredService<IMySingletonService>();
            Console.WriteLine($"MAIN Root Scope After async call - Name: {dataContextService2.UserName} (expecting Patrick) Number: {mySingletonService2.Number}");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
        }

        return 0;
    }

    private static async Task WorkUsesTheScopeItCreateAsync(IHost host, string name, int number)
    {
        using (var scope = host.Services.CreateAsyncScope())
        {
            // Assign values
            var mySingletonService1 = scope.ServiceProvider.GetRequiredService<IMySingletonService>();
            mySingletonService1.Number = number;
            var context1 = scope.ServiceProvider.GetRequiredService<IDataContextService>();
            context1.UserName = name;

            // Log what happens before
            Console.WriteLine($"New Scope Before async call - Name: {context1.UserName} Number: {mySingletonService1.Number}");

            // Async call!!
            var myService1 = scope.ServiceProvider.GetRequiredService<IWorkerService>();
            await myService1.DoWorkAsync();
            
            // Log what happens afterwards
            var context2 = scope.ServiceProvider.GetRequiredService<IDataContextService>();
            var mySingletonService2 = scope.ServiceProvider.GetRequiredService<IMySingletonService>();
            Console.WriteLine($"New Scope After async call - Name: {context2.UserName} (expecting {name}) Number: {mySingletonService2.Number}");

        }
    }

    /// <summary>
    /// Because the scope that was created was not used, you are really interacting with the root scope!!
    /// </summary>
    /// <param name="host"></param>
    /// <param name="name"></param>
    /// <param name="number"></param>
    /// <returns></returns>
    private static async Task WorkUsesRootScopeAsync(IHost host, string name, int number)
    {
        // Assign values
        var mySingletonService1 = host.Services.GetRequiredService<IMySingletonService>();
        mySingletonService1.Number = number;
        var context1 = host.Services.GetRequiredService<IDataContextService>(); // accessing ROOT scope
        context1.UserName = name;

        // Log what happens before
        Console.WriteLine($"Root Scope Before async call - Name: {context1.UserName} Number: {mySingletonService1.Number}");

        // Async call!!
        var myService1 = host.Services.GetRequiredService<IWorkerService>();
        await myService1.DoWorkAsync();

        // Log what happens afterwards
        var context2 = host.Services.GetRequiredService<IDataContextService>(); // accessing ROOT scope
        var mySingletonService2 = host.Services.GetRequiredService<IMySingletonService>();
        Console.WriteLine($"Root Scope After async call - Name: {context2.UserName} (expecting {name}) Number: {mySingletonService2.Number}");
    }
}
 
 
  