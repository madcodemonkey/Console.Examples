using ConsoleAsyncLocal.Services;
using ConsoleAsyncLocal.Services.Extensions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


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
            var context = host.Services.GetRequiredService<IDataContextService>();
            Console.WriteLine("Before: " + context.Peek());

            var work1 = Work1Async(host);
            var work2 = Work2Async(host);
            Task.WaitAll(new[] { work1, work2 });

            Console.WriteLine("After: " + context.Peek());

            Console.WriteLine("Done");
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            Console.WriteLine(ex.StackTrace);
           
        }

        return 0;
    }

    private static async Task Work1Async(IHost host)
    {
        using (var myService1 = host.Services.GetRequiredService<IWorkerService>())
        {
            await myService1.DoWorkAsync("Bob");
            var found1 = myService1.GetData() == "Bob";
            Console.WriteLine($"Bob found: {found1}  Found: {myService1.GetData()} ");

            using (var myService2 = host.Services.GetRequiredService<IWorkerService>())
            {
                await myService2.DoWorkAsync("John");
                var found2 = myService2.GetData() == "John";
                Console.WriteLine($"John found: {found2} Found: {myService2.GetData()} ");
            }

            var found3 = myService1.GetData() == "Bob";
            Console.WriteLine($"Bob found: {found3}  Found: {myService1.GetData()} ");
        }
    }

    private static async Task Work2Async(IHost host)
    {
        using (var myService1 = host.Services.GetRequiredService<IWorkerService>())
        {
            await myService1.DoWorkAsync("Jack");
            var found1 = myService1.GetData() == "Jack";
            Console.WriteLine($"Jack found: {found1}  Found: {myService1.GetData()} ");

            using (var myService2 = host.Services.GetRequiredService<IWorkerService>())
            {
                await myService2.DoWorkAsync("Robert");
                var found2 = myService2.GetData() == "John";
                Console.WriteLine($"Robert found: {found2} Found: {myService2.GetData()} "); 
            }

            var found3 = myService1.GetData() == "Jack";
            Console.WriteLine($"Jack found: {found3} Found: {myService1.GetData()} ");
        }
    }
}
 
 
  