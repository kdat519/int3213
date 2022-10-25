using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace ElGamal
{
    public static class ElGamalService
    {
        public static (BigInteger, BigInteger, BigInteger, BigInteger) KeyGenerate(int length)
        {
            var p = GetSafePrime(length);
            var g = GetGeneratorOfSafePrime(p);
            var a = RandomService.GetRandomInteger(1, p - 1);
            var h = BigInteger.ModPow(g, a, p);
            return (p, g, a, h);
        }
        public static (BigInteger, BigInteger) Encrypt(BigInteger x, BigInteger p, BigInteger g, BigInteger h)
        {
            var k = RandomService.GetRandomInteger(1, p - 1);
            var c1 = BigInteger.ModPow(g, k, p);
            var c2 = (BigInteger.ModPow(h, k, p) * x) % p;
            return (c1, c2);
        }
        public static BigInteger Decrypt(BigInteger c1, BigInteger c2, BigInteger p, BigInteger a)
        {
            return (c2 * ModularService.Inverse(BigInteger.ModPow(c1, a, p), p)) % p;
        }
        private static BigInteger GetSafePrime(int length)
        {
            while (true)
            {
                var p = RandomService.GetRandomPrime(length);
                if (PrimeService.IsPrime((p - 1) / 2)) return p;
            }
        }
        private static BigInteger GetGeneratorOfSafePrime(BigInteger p)
        {
            for (BigInteger i = 2; i < p; i++)
            {
                if (BigInteger.ModPow(i, 2, p) != 1 && BigInteger.ModPow(i, (p - 1) / 2, p) != 1)
                    return i;
            }
            return 0;
        }
    }
}
