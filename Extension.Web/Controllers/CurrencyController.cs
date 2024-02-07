using Extension.Application.AppFactory;
using Extension.Application.Dto;
using Extension.Application.Dto.Base;
using Extension.Domain.Common;
using Extension.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Extension.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CurrencyController : CrudBaseController<int, Currency, CurrencyDto, PagedFullInputDto, CurrencyDto>
    {
        public CurrencyController(IAppFactory factory) : base(factory)
        {

        }
    }
}
