using Bogus;

namespace WebApplication1.Utility.IGenerator;

public interface IGenerator<T> where T : class
{
    Faker<T> Faker { get; }
    IEnumerable<T> Generate(int count);
}