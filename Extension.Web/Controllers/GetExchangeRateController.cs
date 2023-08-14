using Microsoft.AspNetCore.Mvc;
using PCS.Extension.Common;
using Extension.Domain.Entities;
using Extension.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Extension.Infracstructure;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace PCS.Extension.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetExchangeRateController : ControllerBase
    {
        // GET: api/<GetExchangeRateController>
        private readonly ExtensionDbContext extensionDbContext;
        public GetExchangeRateController(ExtensionDbContext ExtensionDbContext)
        {
            extensionDbContext = ExtensionDbContext;
        }
        [HttpGet]
        public async Task<List<Currency>> Get()
        {
            return await GetExchangeVietcombank.GetExchange();
        }
        [HttpPost]
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
                    GetDateTime = item.GetDateTime,
                    CurencyId = item.CurencyId
                };
            }
            return outPut;
        }
    }
}