using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Helper
{
    public static class PrimeService
    {
        public static bool IsPrime(BigInteger value)
        {
            return IsPassMilerRabinTest(value, 200);
        }
        public static bool IsPassMilerRabinTest(BigInteger value, int rounds)
        {
            while (rounds > 0)
            {
                var witness = RandomService.GetRandomInteger(2, value - 2);
                if (!IsProbablyPrime(value, witness))
                {
                    return false;
                }
                rounds--;
            }
            return true;
        }
        private static bool IsProbablyPrime(BigInteger value, BigInteger witness)
        {
            if (value.IsEven || BigInteger.GreatestCommonDivisor(value, witness) > 1)
                return false;
            (BigInteger, BigInteger) Factorize(BigInteger value)
            {
                BigInteger k = 0;
                while (value.IsEven)
                {
                    value /= new BigInteger(2);
                    k += 1;
                }
                return (k, value);
            }
            var (k, q) = Factorize(value - 1);
            witness = BigInteger.ModPow(witness, q, value);
            if ((witness - 1) % value == 0)
                return true;
            for (var i = 0; i < k; i++)
            {
                if ((witness + 1) % value == 0)
                    return true;
                witness = BigInteger.ModPow(witness, 2, value);
            }
            return false;
        }
    }
}
