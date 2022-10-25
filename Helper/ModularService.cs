using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class ModularService
    {
        public static BigInteger Inverse(BigInteger value, BigInteger modulus)
        {
            var (s, _, r) = ExtendedEuclidean(value, modulus);
            if (r != 1) throw new ArithmeticException($"GCD({value}, {modulus}) != 1");
            return Reduce(s, modulus);
        }
        public static BigInteger Reduce(BigInteger value, BigInteger modulus)
        {
            Debug.Assert(modulus > 0);
            var remainder = value % modulus;
            return (remainder < 0) ? remainder + modulus : remainder;
        }
        private static (BigInteger, BigInteger, BigInteger) ExtendedEuclidean(BigInteger a, BigInteger b)
        {
            (BigInteger oldR, BigInteger r) = (BigInteger.Abs(a), BigInteger.Abs(b));
            (BigInteger oldS, BigInteger s) = (1, 0);
            (BigInteger oldT, BigInteger t) = (0, 1);

            while (r != 0)
            {
                BigInteger quotient = oldR / r;
                (oldR, r) = (r, oldR - r * quotient);
                (oldS, s) = (s, oldS - s * quotient);
                (oldT, t) = (t, oldT - t * quotient);
            }
            return (oldS * a.Sign, oldT * b.Sign, oldR);
        }
    }
}
