using Extension.Common;
using Extension.Domain.Entities;
using Extension.Infrastructure;
using Extension.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Extension.Controllers
{
    [ApiController]
    public class ExchangeRateController : BaseController
    {
        // GET: api/<GetExchangeRateController>
        private readonly ExtensionDbContext extensionDbContext;
        public ExchangeRateController(ExtensionDbContext ExtensionDbContext)
        {
            extensionDbContext = ExtensionDbContext;
        }

        [HttpGet("get-list")]
        public async Task<List<Currency>> Post()
        {
            DeserializeJsonCurrency deserialize = new();
            var outPut = await deserialize.DeserializeJson();
            foreach (var item in outPut)
            {
                Currency currency = new()
                {
                    CurrencyCode = item.CurrencyCode,
                    CurrencyName = item.CurrencyName,
                    ExchangeRate = item.ExchangeRate,
                    Id = item.Id
                };
            }
            return outPut;
        }

        [HttpPost("get-list-realtime")]
        public async Task<List<Currency>> GetListExchange()
        {
            return await GetExchangeVietcombank.GetExchange();
        }
    }
}