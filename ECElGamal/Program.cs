using System.Security.Cryptography;

namespace ECElGamal
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var (curve, a, pointP, pointQ) = ECElGamalService.KeyGenerate(6);
            var random = new Random();
            Console.WriteLine(curve);
            // Point: (bool IsAtInfinity, BigInteger x, BigInteger y)
            var x = curve.points[random.Next(2, curve.points.Count)];
            var (c1, c2) = ECElGamalService.Encrypt(x, pointP, pointQ, curve);
            var d = ECElGamalService.Decrypt(c1, c2, curve, a);
            Console.WriteLine($"Message = {x}\nEncrypt = {(c1, c2)}\nDecrypt = {d}");
        }
    }
}