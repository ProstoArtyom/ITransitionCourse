namespace ConsoleApp1.HelperClasses.Extensions;
public static class BytesExtensions
{
    public static string ToHex(this byte[] bytes)
    {
        return string.Concat(bytes.Select(b => b.ToString("x2")));
    }
}