using Extension.Web;
using Hangfire;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

public class DomainRegistrationJob
{
    private readonly DomainSdk _domainSdk;
    private readonly IMemoryCache _memoryCache;

    public DomainRegistrationJob(DomainSdk domainSdk, IMemoryCache memoryCache)
    {
        _domainSdk = domainSdk;
        _memoryCache = memoryCache;
    }

    [AutomaticRetry(OnAttemptsExceeded = AttemptsExceededAction.Delete)]
    public async Task RegisterDomain(string domain)
    {
        var isActive = await _domainSdk.FetchListDomain(domain);
        
        if (isActive)
        {
            // get background job id form memory cache
            if (_memoryCache.TryGetValue($"RegistrationJobId{domain}", out var cachedJobId))
            {
                AddAutoContract(domain);
                BackgroundJob.Delete((string)cachedJobId);
                Console.WriteLine("Background job have been removed!");
            }
        }
    }

    public void AddAutoContract(string domain)
    {
        for (int i = 0; i < 5; i++)
        {
            Task.Delay(1000);
            Console.WriteLine("Adding contract ....!" + (i + 1) + "s");
            i++;
        }
        Console.WriteLine("Add contract success!");
    }
}
