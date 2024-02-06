using System.Security.Cryptography;

namespace ConsoleApp1;
class Program
{
    public static void Main(string[] args)
    {
        if (!ArgsValidation.IsArgsValid(args, out string message))
        {
            Console.WriteLine(message);
            return;
        }
    }
}