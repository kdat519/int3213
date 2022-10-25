using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Helper;

namespace ECElGamal
{
    public class EllipticCurve
    {
        BigInteger p, a, b;
        public List<(bool IsAtInfinity, BigInteger x, BigInteger y)> points { get; } = new();

        public EllipticCurve(BigInteger p, BigInteger a, BigInteger b)
        {
            (this.p, this.a, this.b) = (p, a, b);
            FindAllPoint();
        }

        void FindAllPoint()
        {
            points.Add((true, 0, 0));
            Dictionary<BigInteger, List<BigInteger>> quadratic = new();
            for (BigInteger y = 0; y < p; y++)
            {
                var left = y * y % p;
                if (quadratic.ContainsKey(left))
                {
                    quadratic[left].Add(y);
                }
                else
                {
                    quadratic.Add(left, new List<BigInteger> { y });
                }
            }
            for (BigInteger x = 0; x < p; x++)
            {
                var right = (BigInteger.ModPow(x, 3, p) + a * x + b) % p;
                if (quadratic.ContainsKey(right))
                {
                    foreach (var y in quadratic[right])
                    {
                        points.Add((false, x, y));
                    }
                }
            }
        }
        public (bool IsAtInfinity, BigInteger x, BigInteger y) Add(
            (bool, BigInteger, BigInteger) point1,
            (bool, BigInteger, BigInteger) point2)
        {
            var (IsAtInfinity1, x1, y1) = point1;
            var (IsAtInfinity2, x2, y2) = point2;

            if (IsAtInfinity1) return point2;
            if (IsAtInfinity2) return point1;

            if (x1 == x2 && (y1 + y2) % p == 0)
                return (true, 0, 0);

            var lambda = (x1 == x2 && y1 == y2)
                ? (3 * x1 * x1 + a) * ModularService.Inverse(2 * y1, p)
                : (y2 - y1) * ModularService.Inverse(x2 - x1, p);

            var x3 = ModularService.Reduce((lambda * lambda - x1 - x2), p);
            var y3 = ModularService.Reduce((lambda * (x1 - x3) - y1), p);
            return (false, x3, y3);
        }
        public (bool IsAtInfinity, BigInteger x, BigInteger y) Multiply(
            BigInteger n, (bool, BigInteger, BigInteger) point)
        {
            (bool, BigInteger, BigInteger) result = (true, 0, 0);
            (bool, BigInteger, BigInteger) power = point;
            while (n > 0)
            {
                if (!n.IsEven)
                {
                    result = Add(result, power);
                }
                    
                power = Add(power, power);
                n /= 2;
            }
            return result;
        }
        public override string ToString()
        {
            return $"EllipticCurve(a = {a}, b = {b}, p = {p}) Points: {points.Count}";
        }
    }
}
