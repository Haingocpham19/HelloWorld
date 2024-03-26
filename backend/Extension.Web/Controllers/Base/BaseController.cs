using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Controllers.Base
{
    /// <summary>
    /// Base controller for our web API.
    /// </summary>
    [Authorize]
    [ApiController]
    [Produces("application/json")]
    [Route("~/app-api/[controller]")]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController() : base()
        {
               
        }
    }
}
