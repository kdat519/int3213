using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace RSA
{
    public class RSAService
    {
        public static (BigInteger, BigInteger, BigInteger) KeyGenerate(int length)
        {
            BigInteger e = 65537;
            BigInteger p, q, phi;
            do
            {
                p = RandomService.GetRandomPrime(length);
                q = RandomService.GetRandomPrime(length + 2);
                phi = (p - 1) * (q - 1);
                Debug.Assert(phi > e);
            }
            while (p == q || BigInteger.GreatestCommonDivisor(e, phi) != 1);
            BigInteger d = ModularService.Inverse(e, phi);
            BigInteger n = p * q;
            return (n, e, d);
        }
        public static BigInteger Encrypt(BigInteger x, BigInteger e, BigInteger n)
        {
            return BigInteger.ModPow(x, e, n);
        }
        public static BigInteger Decrypt(BigInteger y, BigInteger d, BigInteger n)
        {
            return BigInteger.ModPow(y, d, n);
        }
    }
}
