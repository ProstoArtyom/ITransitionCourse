using System.Security.Cryptography;
using System.Text;
using ConsoleApp1.HelperClasses.Extensions;
using SHA3.Net;

namespace ConsoleApp1.HelperClasses;
public class HMACHelper
{
    public string AESKey { get; } = GetAESKey();

    private static string GetAESKey()
    {
        using Aes aesAlgorithm = Aes.Create();

        aesAlgorithm.KeySize = 256;
        aesAlgorithm.GenerateKey();

        string keyBase64 = Convert.ToBase64String(aesAlgorithm.Key);

        return keyBase64;
    }

    public string GetHMAC(string move)
    {
        byte[] toHash = Encoding.ASCII.GetBytes(move + AESKey);
        string hash = Sha3.Sha3256().ComputeHash(toHash).ToHex();

        return hash;
    }
}