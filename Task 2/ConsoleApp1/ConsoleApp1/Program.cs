using System.Reflection;
using System.Text;
using System.Text.Encodings;
using SHA3.Net;

namespace ConsoleApp1;
class Program
{
    public static void Main()
    {
        var fileNames = Directory.GetFiles("../../../files/");
        var hashList = new List<string>();
        foreach (var filePath in fileNames)
        {
            var fileBytes = File.ReadAllBytes(filePath);
            string hash = Sha3.Sha3256().ComputeHash(fileBytes).ToHex();
            hashList.Add(hash);
        }

        var hashesWithEmail = string.Join("", hashList.OrderBy(x => x)) + "artyom.shashok@mail.ru";
        var result = Sha3.Sha3256().ComputeHash(Encoding.ASCII.GetBytes(hashesWithEmail)).ToHex();
        Console.WriteLine(result);
    }
}

public static class BytesExtensions
{
    public static string ToHex(this byte[] bytes)
    {
        return string.Concat(bytes.Select(b => b.ToString("x2")));
    }
}