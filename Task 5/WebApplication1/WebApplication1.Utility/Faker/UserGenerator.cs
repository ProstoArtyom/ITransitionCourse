using Bogus;
using WebApplication1.Models;
using WebApplication1.Utility.Faker.Extensions;

namespace WebApplication1.Utility.Faker;

public class UserGenerator : Generator<User>
{
    public List<User> Users { get; set; }
    public float ErrorFactor { get; set; }
    public int ErrorSeed { get; set; }

    public UserGenerator(string region = "en_US", int seed = 0)
    {
        Initialize(region, seed);
    }

    public override void Initialize(string region, int seed)
    {
        base.Initialize(region, seed);
        Faker.RuleFor(u => u.Id, f => f.Random.Guid().ToString())
            .RuleFor(u => u.Number, f => ++f.IndexVariable)
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.FullAddress, f => f.Address.GetFullAddress())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());
    }

    public override IEnumerable<User> Generate(int count = 1)
    {
        return base.Generate(count).GenerateErrorUsers(ErrorFactor, ErrorSeed);
    }

    public override void Reset()
    {
        Initialize(Region, Seed);
    }

    public void SetValues(string region, int seed, float errorFactor, int errorSeed)
    {
        Region = region;
        Seed = seed;
        ErrorFactor = errorFactor;
        ErrorSeed = errorSeed;
        
        Reset();
    }

    public void ChangeSeed(int key)
    {
        Faker.UseSeed(Seed + key);
    }
}