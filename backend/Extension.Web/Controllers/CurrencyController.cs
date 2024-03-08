using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Application.Dto.Base;
using Extension.Domain.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Controllers
{
    [Authorize]
    [Route("~/private-api/[controller]")]
    [ApiController]
    public class CurrencyController : CrudBaseController<int, Currency, CurrencyDto, PagedFullInputDto, CurrencyDto>
    {
        public CurrencyController(IAppFactory factory) : base(factory)
        {

        }
    }
}
