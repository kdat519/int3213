using Helper;
using System.Numerics;

namespace ElGamal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var (p, g, a, h) = ElGamalService.KeyGenerate(50);
            BigInteger x = RandomService.GetRandomInteger(0, p - 1);
            var (c1, c2) = ElGamalService.Encrypt(x, p, g, h);
            var x2 = ElGamalService.Decrypt(c1, c2, p, a);
            Console.WriteLine($"p = {p}\ng = {g}\na = {a}\nh = {h}\n");
            Console.WriteLine($"Message = {x}\nEncrypt = ({c1}, {c2})\nDecrypt = {x2}");
        }
    }
}