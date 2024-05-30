using ConsoleDependencyInjectionExp1.Services.Options;
using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionExp1.Services;

public class MathService : IMathService
{
    private readonly string _someTextSetting;

    /// <summary>Constructor</summary>
    public MathService(IOptions<ServiceOptions> options)
    {
        _someTextSetting = options.Value.SomeText;
    }

    /// <inheritdoc/>
    public int Add(int a, int b)
    {
        Console.WriteLine(_someTextSetting);
        return a + b;
    }
}