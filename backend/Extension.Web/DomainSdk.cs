using Hangfire;
using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace Extension.Web
{
    public class DomainSdk
    {
        public async Task<bool> FetchListDomain(string domain, int count = 0)
        {
            Console.WriteLine($"Domain: {domain} Creating ... :" + DateTime.Now.ToString("hh:mm:ss"));

            if (count == 30)
            {
                Console.WriteLine("Domain Create success !!!!!");
                return true; // Return success
            }

            await Task.Delay(1000);

            return await FetchListDomain(domain, count + 1);
        }
    }
}
