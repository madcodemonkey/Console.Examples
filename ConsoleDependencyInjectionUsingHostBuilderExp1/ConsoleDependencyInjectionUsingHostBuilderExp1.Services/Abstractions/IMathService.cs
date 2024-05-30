namespace ConsoleDependencyInjectionUsingHostBuilderExp1.Services;

public interface IMathService
{
    /// <summary>
    /// Adds two numbers together.
    /// </summary>
    /// <param name="a">The first integer</param>
    /// <param name="b">The second integer</param>
    /// <returns>Sum</returns>
    Task<int> AddAsync(int a, int b);
}