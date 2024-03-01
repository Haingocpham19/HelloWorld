using Hangfire;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;

namespace Extension.Web.Controllers
{
    public class TestController : Controller
    {
        private readonly IMemoryCache _memoryCache;

        public TestController(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        [HttpGet("/api/register-domain")]
        public IActionResult RegisterDomain(string domain)
        {
            var jobId = BackgroundJob.Enqueue<DomainRegistrationJob>(x => x.RegisterDomain(domain));

            // save jobId on memory cache
            var cacheEntryOptions = new MemoryCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(30) // expired after 30 minutes
            };

            // Using HttpContext.RequestServices to get MemoryCache from DI container
            //var memoryCache = HttpContext.RequestServices.GetRequiredService<IMemoryCache>();
            _memoryCache.Set($"RegistrationJobId{domain}", jobId, cacheEntryOptions);

            // Trả về response hoặc thực hiện các công việc khác
            return Ok("Domain registration request received.");
        }
    }
}
