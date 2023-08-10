//using Microsoft.AspNetCore.Mvc;
//using PCS.Extension.Common;
//using Extension.Domain.EF;
//using Extension.Domain.Entities;
//using Extension.Domain.Repositories;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace PCS.Extension.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class GetExchangeRateController : ControllerBase
//    {
//        // GET: api/<GetExchangeRateController>
//        private readonly IResponseDataRepository _reponseDataRepository;
//        private readonly ExtensionDbContext _ExtensionDbContext;
//        public GetExchangeRateController(IResponseDataRepository reponseDataRepository, ExtensionDbContext ExtensionDbContext)
//        {
//            _reponseDataRepository = reponseDataRepository;
//            _ExtensionDbContext = ExtensionDbContext;
//        }
//        [HttpGet]
//        public async Task<List<Currency>> Get()
//        {
//            GetExchangeVietcombank temp = new GetExchangeVietcombank();
//            var result = temp.GetExchange().Result;
//            return result;
//        }
//        [HttpPost]
//        public async Task<List<Currency>> Post()
//        {
//            DeserializeJsonCurrency deserialize = new DeserializeJsonCurrency();
//            var outPut = deserialize.DeserializeJson().Result;
//            foreach (var item in outPut)
//            {
//                Currency currency = new Currency()
//                {
//                    CurrencyCode = item.CurrencyCode,
//                    CurrencyName = item.CurrencyName,
//                    ExchangeRate = item.ExchangeRate,
//                    GetDateTime = item.GetDateTime,
//                    CurencyId = item.CurencyId
//                };
//                _reponseDataRepository.InsertCurrency(currency);
//            }
//            _reponseDataRepository.Save();
//            return outPut;
//        }
//    }
//}