using System;
using System.Numerics;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;

[ApiController]
public class PiController : Microsoft.AspNetCore.Mvc.Controller
{
    private readonly IDistributedCache _cache;

    public PiController(IDistributedCache cache)
    {
        this._cache = cache;
    }

    [HttpGet("pi/{digits=25}/{iterations:int=10000}")]
    public async Task<IActionResult> GoAsync(int digits, int iterations)
    {
        var key = $"pi_{digits}_{iterations}";
        
        // add to cache
        var checkCache = await _cache.GetAsync(key);

        if (checkCache == null)
        {
            PiCalculator piCalculator = new PiCalculator();
            var result = piCalculator.GetPi(digits, iterations);

            await _cache.SetAsync(key, result.ToByteArray(), new DistributedCacheEntryOptions()
            {
                AbsoluteExpiration = DateTimeOffset.UtcNow.AddMinutes(1)
            });
            
            return Ok(result.ToString());
        }
        else
        {
            var result = new BigInteger(checkCache);
            return Ok(result.ToString());
        }
    }
}