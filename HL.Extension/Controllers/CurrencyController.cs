using Extension.Domain.Entities;
using Extension.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using PCS.Extension.Services;

namespace PCS.Extension.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CurrencyController : BaseCustomController<Currency>
    {
        public CurrencyController(IBaseService<Currency> baseService) : base(baseService)
        {
        }
    }
}
