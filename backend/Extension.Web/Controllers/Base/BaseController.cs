using Microsoft.AspNetCore.Mvc;

namespace Extension.Web.Controllers.Base
{
    /// <summary>
    /// Base controller for our web API.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    [Produces("application/json")]
    public abstract class BaseController : ControllerBase
    {
        protected BaseController() : base()
        {
               
        }
    }
}
