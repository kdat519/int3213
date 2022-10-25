using Helper;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ECElGamal
{
    public static class ECElGamalService
    {
        public static (
            EllipticCurve, 
            BigInteger,
            (bool, BigInteger, BigInteger), 
            (bool, BigInteger, BigInteger)) KeyGenerate(int length)
        {
            var curve = GetCurve(length);
            var order = curve.points.Count;
            var a = RandomService.GetRandomInteger(1, order);
            var pointP = curve.points[1];
            var pointQ = curve.Multiply(a, pointP);
            return (curve, a, pointP, pointQ);
        }
        static EllipticCurve GetCurve(int length)
        {
            EllipticCurve curve;
            BigInteger a, b;
            while (true)
            {
                var p = RandomService.GetRandomPrime(length);
                for (a = 100; a >= 0; a--)
                {
                    for (b = 100; b >= 0; b--)
                    {
                        if ((4 * a * a * a + 27 * b * b) % p == 0) continue;
                        curve = new EllipticCurve(p, a, b);
                        if (PrimeService.IsPrime(curve.points.Count)) return curve;
                    }
                }
            }
        }
        public static ((bool, BigInteger, BigInteger), (bool, BigInteger, BigInteger)) Encrypt(
            (bool, BigInteger, BigInteger) x, 
            (bool, BigInteger, BigInteger) pointP,
            (bool, BigInteger, BigInteger) pointQ,
            EllipticCurve curve)
        {
            var order = curve.points.Count;
            var k = RandomService.GetRandomInteger(1, order);
            var c1 = curve.Multiply(k, pointP);
            var c2 = curve.Add(x, curve.Multiply(k, pointQ));
            return (c1, c2);
        }
        public static (bool, BigInteger, BigInteger) Decrypt(
            (bool, BigInteger, BigInteger) c1,
            (bool, BigInteger, BigInteger) c2,
            EllipticCurve curve,
            BigInteger a)
        {
            var (IsAtInfinity, x, y) = curve.Multiply(a, c1);
            return curve.Add(c2, (IsAtInfinity, x, -y));
        }
        
    }
}
