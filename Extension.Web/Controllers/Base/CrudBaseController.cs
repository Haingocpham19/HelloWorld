using Microsoft.AspNetCore.Mvc;
using Extension.Services;
using System.Threading.Tasks;
using Extension.Application.Dto.Base;

namespace Extension.Web.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CrudBaseController<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> : ControllerBase
        where TEntity : class
        where TEntityDto : class
        where TPagedInput : PagedFullInputDto
        where TPagedOutput : class//, IEntity<TPrimaryKey>
    {
        protected readonly IBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> _baseService;

        public CrudBaseController(IBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> baseService)
        {
            _baseService = baseService;
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetById(TPrimaryKey Id)
        {
            var result = await _baseService.GetByIdAsync(Id);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Create(TEntityDto input)
        {
            var result = await _baseService.InsertAsync(input);
            return Ok(result);
        }

        [HttpPost]
        public virtual async Task<IActionResult> Update(TEntityDto input)
        {
            var result = await _baseService.UpdateAsync(input);
            return Ok(result);
        }
    }
}
