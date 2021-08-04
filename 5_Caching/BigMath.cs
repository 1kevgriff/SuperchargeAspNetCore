using System.Collections.Generic;
using System.Numerics;

public static class BigMath
{
    //arctan(x) = x - x^3/3 + x^5/5 - x^7/7 + x^9/9 - ...
    public static IEnumerable<BigInteger> ArcTan1OverX(int x, int digits)
    {
        var mag = BigInteger.Pow(10, digits);
        var sum = BigInteger.Zero;
        bool sign = true;
        for (int i = 1; true; i += 2)
        {
            var cur = mag / (BigInteger.Pow(x, i) * i);
            if (sign)
            {
                sum += cur;
            }
            else
            {
                sum -= cur;
            }
            yield return sum;
            sign = !sign;
        }
    }
}