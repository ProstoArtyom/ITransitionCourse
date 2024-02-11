using Bogus;

namespace WebApplication1.Utility.Faker.IGenerator;

public interface IGenerator<T> where T : class
{
    IEnumerable<T> Generate(int count);
}