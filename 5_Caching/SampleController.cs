
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;

[ApiController]
public class PersonController : Controller
{
    private readonly IDistributedCache _cache;

    public SampleController(IDistributedCache cache)
    {
        this._cache = cache;
    }

    public async Task<IActionResult> GoAsync()
    {

        // add to cache
        _cache.SetAsync("user_8675309", new Person { Name = "Jenny" });

        // set from cache
        var result = await _cache.GetAsync("user_8675309");

        return Ok();
    }
}