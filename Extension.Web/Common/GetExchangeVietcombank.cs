using Extension.Domain.Entities;
using Newtonsoft.Json;
using PuppeteerSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PCS.Extension.Common
{
    public static class GetExchangeVietcombank
    {
        public static async Task<List<Currency>> GetExchange()
        {
            List<Currency> listResult = new();
            var options = new LaunchOptions
            {
                Headless = false,
            };
            //Console.WriteLine("Downloading chromium");
            //Download the Chromium revision if it does not already exist
            await new BrowserFetcher().DownloadAsync();

            using (var browser = await Puppeteer.LaunchAsync(options))

            using (var page = await browser.NewPageAsync())
            {
                string getDate = DateTime.Now.ToString("dd/MM/yy");

                string url = "https://portal.vietcombank.com.vn/UserControls/TVPortal.TyGia/pListTyGia.aspx?txttungay=" + getDate + "&BacrhID=1&isEn=True";

                await page.GoToAsync(url);
                await page.WaitForTimeoutAsync(100);
                int countCurrency = await page.EvaluateExpressionAsync<int>("document.querySelectorAll('.odd').length");
                for (int i = 0; i < countCurrency; i++)
                {
                    Currency temp = new()
                    {
                        CurencyId = i + 1,
                        CurrencyName = await page.EvaluateExpressionAsync<string>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(1)\").innerText"),
                        CurrencyCode = await page.EvaluateExpressionAsync<string>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(2)\").innerText"),
                        ExchangeRate = await page.EvaluateExpressionAsync<decimal>($"document.querySelector(\"#ctl00_Content_ExrateView > tbody > tr:nth-child({i + 3}) > td:nth-child(4)\").innerText")
                    };
                    listResult.Add(temp);
                }
                await page.CloseAsync();
            }

            // Folder, where a file is created.  
            // Make sure to change this folder to your own folder  
            string folder = Directory.GetCurrentDirectory();
            // Filename  
            const string fileName = "SaveJsonFile\\InfoCurrencyEveryDay.json";
            // Fullpath. You can direct hardcode it if you like. 
            // Read a file  
            string fullPath = Path.Combine(folder, fileName);
            // write JSON directly to a file
            string json = JsonConvert.SerializeObject(listResult);
            System.IO.File.WriteAllText(fullPath, json);
            Console.WriteLine("Completed");

            return listResult;
        }
    }
}
