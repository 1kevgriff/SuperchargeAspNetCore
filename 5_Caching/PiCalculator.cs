using System.Linq;
using System.Numerics;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Caching.Memory;

/// <summary>
/// Borrowed from https://gist.github.com/quietlynn/2028653
/// </summary>
public class PiCalculator
{
    // Pi = 16 * arctan(1/5) - 4 * arctan(1/239)
    public BigInteger GetPi(int digits, int iterations)
    {
        return 16 * BigMath.ArcTan1OverX(5, digits).ElementAt(iterations)
               - 4 * BigMath.ArcTan1OverX(239, digits).ElementAt(iterations);
    }
}