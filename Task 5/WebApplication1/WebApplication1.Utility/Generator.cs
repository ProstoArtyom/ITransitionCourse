using Bogus;
using WebApplication1.Models;
using WebApplication1.Utility.IGenerator;

namespace WebApplication1.Utility;

public class Generator<T> : IGenerator<T> where T : class
{
    public Faker<T> Faker { get; }

    public Generator(string locale, int seed)
    {
        Faker = new Faker<T>(locale)
            .UseSeed(seed);
    }

    public virtual IEnumerable<T> Generate(int count = 1)
    {
        return Faker.Generate(count);
    }
}