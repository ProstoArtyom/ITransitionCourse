using Bogus.DataSets;

namespace WebApplication1.Utility.Faker.Extensions;

public static class FakerExtensions
{
    public static string GetFullAddress(this Address address)
    {
        return string.Join(", ", address.City(), address.StreetAddress(), address.SecondaryAddress());
    }
}