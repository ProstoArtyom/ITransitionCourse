using Bogus;
using WebApplication1.Models;
using WebApplication1.Utility.Extensions;
using WebApplication1.Utility.IGenerator;

namespace WebApplication1.Utility;

public class UserGenerator : Generator<User>
{
    public UserGenerator(string locale, int seed) : base(locale, seed)
    {
        Faker.RuleFor(u => u.Id, f => f.Random.Guid().ToString())
            .RuleFor(u => u.Number, f => f.IndexVariable++)
            .RuleFor(u => u.Name, f => f.Name.FullName())
            .RuleFor(u => u.StreetAddress, f => f.Address.GetFullAddress())
            .RuleFor(u => u.PhoneNumber, f => f.Phone.PhoneNumber());
    }
    
    
}