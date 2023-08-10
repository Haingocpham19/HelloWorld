using Extension.Domain.Entities;
using Extension.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using PCS.Extension.Services;
using PCS.Extension.Services.implement;
using PCS.Extension.Services.interfaces;

namespace PCS.Extension.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ClientCardController : BaseCustomController<ClientCard>
    {
        private readonly IClientCardService _clientCardService;
        public ClientCardController(IBaseService<ClientCard> baseService, IClientCardService clientCardService) : base(baseService)
        {
            _clientCardService = clientCardService;
        }
        [HttpPost]
        public IActionResult AddClient(ClientCard entity)
        {
            entity.CreateDate = System.DateTime.Now;
            var result = _clientCardService.InsertClient(entity);
            return Ok(result);
        }
    }
}
