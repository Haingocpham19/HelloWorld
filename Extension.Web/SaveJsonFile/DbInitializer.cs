using Extension.Domain.Entities;
using Extension.Infracstructure;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PCS.Extension.SaveJsonFile
{
    public static class DbInitializer
    {
        public static async Task SeedData(ExtensionDbContext DbContext)
        {
            if (!DbContext.Currencies.Any())
            {
                string currencysJson = File.ReadAllText("SaveJsonFile" + Path.DirectorySeparatorChar + "InfoCurrencyEveryDay.json");
                List<Currency> currencyList = JsonConvert.DeserializeObject<List<Currency>>(currencysJson);
                await DbContext.Currencies.AddRangeAsync(currencyList);
                await DbContext.SaveChangesAsync();
            }
            if (!DbContext.SourcePages.Any())
            {
                string currencysJson = System.IO.File.ReadAllText("SaveJsonFile" + Path.DirectorySeparatorChar + "InfoSourcePage.json");
                List<SourcePage> sourcePageList = JsonConvert.DeserializeObject<List<SourcePage>>(currencysJson);
                await DbContext.SourcePages.AddRangeAsync(sourcePageList);
                await DbContext.SaveChangesAsync();
            }
        }
    }
}
