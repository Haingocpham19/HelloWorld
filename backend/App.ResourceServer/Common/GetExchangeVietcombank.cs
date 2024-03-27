using Extension.Domain.Entities;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace Extension.Common
{
    public static class GetExchangeVietcombank
    {
        [Obsolete]
        public static async Task<List<Currency>> GetExchange()
        {
            var options = new LaunchOptions
            {
                Headless = true,
                ExecutablePath = "C:\\Program Files\\Google\\Chrome\\Application\\chrome.exe",
            };

            await new BrowserFetcher().DownloadAsync(BrowserFetcher.DefaultRevision);

            using var browser = await Puppeteer.LaunchAsync(options);
            using var page = await browser.NewPageAsync();

            string getDate = DateTime.Now.ToString("dd/MM/yy");
            string url = "https://portal.vietcombank.com.vn/UserControls/TVPortal.TyGia/pListTyGia.aspx?txttungay=" + getDate + "&BacrhID=1&isEn=True";

            dynamic outPut = null;
            await page.GoToAsync(url);
            await page.WaitForTimeoutAsync(100);

            outPut = ExtractCurrencyData(page);
            SaveCurrencyDataToJson(outPut);
            
            await page.CloseAsync();

            return outPut;
        }

        private static List<Currency> ExtractCurrencyData(Page page)
        {
            int countCurrency = page.EvaluateExpressionAsync<int>("document.querySelectorAll('.odd').length").Result;
            var listResult = new List<Currency>();

            for (int i = 0; i < countCurrency; i++)
            {
                var temp = new Currency
                {
                    Id = i + 1,
                    CurrencyName = page.EvaluateExpressionAsync<string>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(1)\").innerText").Result,
                    CurrencyCode = page.EvaluateExpressionAsync<string>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(2)\").innerText").Result,
                    ExchangeRate = page.EvaluateExpressionAsync<decimal>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(4)\").innerText").Result,
                };
                listResult.Add(temp);
            }

            return listResult;
        }

        private static void SaveCurrencyDataToJson(List<Currency> listResult)
        {
            string folder = Directory.GetCurrentDirectory();
            const string fileName = "SaveJsonFile\\InfoCurrencyEveryDay.json";
            string fullPath = Path.Combine(folder, fileName);
            string json = JsonConvert.SerializeObject(listResult);
            System.IO.File.WriteAllText(fullPath, json);
            Console.WriteLine("Completed");
        }
    }
}
