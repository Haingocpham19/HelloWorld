//using Extension.Domain.EF;
//using Extension.Domain.Entities;
//using Extension.Domain.Repositories;
//using Microsoft.AspNetCore.Mvc;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
//using Microsoft.AspNetCore.Authorization;

//// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

//namespace PCS.Extension.Controllers
//{
//    [Route("api/[controller]")]
//    [ApiController]
//    public class ApiController : ControllerBase
//    {
//        private readonly IResponseDataRepository _reponseDataRepository;
//        private readonly ExtensionDbContext _ExtensionDbContext;
//        public ApiController(IResponseDataRepository reponseDataRepository, ExtensionDbContext ExtensionDbContext)
//        {
//            _reponseDataRepository = reponseDataRepository;
//            _ExtensionDbContext = ExtensionDbContext;
//        }
//        //[Route("listall")]
//        //[HttpPost]
//        //public IEnumerable<ResponseData> ListAll()
//        //{
//        //    return _reponseDataRepository.GetAll("haideptrai");
//        //}
//        // POST api/<AmazonApiController>

//        [HttpPost]
//        public async Task<ActionResult> Post(Products reponseData)
//        {

//            //Products response = new Products()
//            //{
//            //    ProductTitle = reponseData.ProductTitle,
//            //    Price = reponseData.Price,
//            //    Availability = reponseData.Availability,
//            //    ProductImageSrc = reponseData.ProductImageSrc,
//            //    Url = reponseData.Url,
//            //    GetDate = DateTime.Now,
//            //    Status = Data.CommanConstant.Contants.PENDING,
//            //    IdPage = reponseData.IdPage,
//            //    IdCurrency = reponseData.IdCurrency,
//            //    UserName = "haideptrai"
//            //};
           

//            return Ok();
//        }
//    }
//}
