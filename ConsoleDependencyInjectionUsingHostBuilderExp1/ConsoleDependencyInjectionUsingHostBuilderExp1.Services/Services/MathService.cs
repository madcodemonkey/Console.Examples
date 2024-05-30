using ConsoleDependencyInjectionUsingHostBuilderExp1.Services.Options;
using Microsoft.Extensions.Options;

namespace ConsoleDependencyInjectionUsingHostBuilderExp1.Services;

public class MathService : IMathService
{
    private readonly string _someTextSetting;

    /// <summary>Constructor</summary>
    public MathService(IOptions<ServiceOptions> options)
    {
        _someTextSetting = options.Value.SomeText;
    }

    /// <inheritdoc/>
    public async Task<int> AddAsync(int a, int b)
    {
        Console.WriteLine(_someTextSetting);
        return await Task.FromResult(a + b);
    }
}