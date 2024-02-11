using Bogus;
using WebApplication1.Models;
using WebApplication1.Utility.Faker.IGenerator;

namespace WebApplication1.Utility.Faker;

public class Generator<T> : IGenerator<T> where T : class
{
    protected Faker<T> Faker { get; set; }
    public string Region { get; set; }
    public int Seed { get; set; }
    public Generator(string region = "en_US", int seed = 0)
    {
        Initialize(region, seed);
    }

    public virtual void Initialize(string region, int seed)
    {
        Faker = new Faker<T>(Region = region)
            .StrictMode(true)
            .UseSeed(Seed = seed);
    }
    
    public virtual IEnumerable<T> Generate(int count = 1)
    {
        return Faker.Generate(count);
    }
    
    public virtual void Reset()
    {
        Initialize(Region, Seed);
    }
}