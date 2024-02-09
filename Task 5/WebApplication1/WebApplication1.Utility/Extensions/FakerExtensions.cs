using Bogus.DataSets;

namespace WebApplication1.Utility.Extensions;

public static class FakerExtensions
{
    public static string GetFullAddress(this Address address)
    {
        return String.Join(", ", address.StreetAddress(), address.SecondaryAddress());
    }
}