
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Diagnostics.HealthChecks;

public class IsWebsiteUpCheck : IHealthCheck
{
    private readonly HttpClient _client;

    public IsWebsiteUpCheck(HttpClient client)
    {
        this._client = client;
    }
    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        var url = "https://consultwithgriff.com";
        var stopWatch = new Stopwatch();

        stopWatch.Start();
        var response = await _client.GetAsync(url);
        stopWatch.Stop();

        if (response.IsSuccessStatusCode && stopWatch.ElapsedMilliseconds < 2000)
        {
            return HealthCheckResult.Healthy("Website is running.");
        }
        else if (response.IsSuccessStatusCode)
        {
            return HealthCheckResult.Degraded("Website is running, but performance is degraded.");
        }
        
        return HealthCheckResult.Unhealthy("Website is not responding correctly.");
    }
}