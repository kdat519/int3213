using System.Numerics;
using Helper;

namespace RSA
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var (n, e, d) = RSAService.KeyGenerate(100);
            Console.WriteLine($"n = {n}\ne = {e}\nd = {d}\n");
            BigInteger x = RandomService.GetRandomInteger(0, n - 1);
            BigInteger y = RSAService.Encrypt(x, e, n);
            Console.WriteLine($"Message = {x}\nEncrypt = {y}\nDecrypt = {RSAService.Decrypt(y, d, n)}");
        }
    }
}