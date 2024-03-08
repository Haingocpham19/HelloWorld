using AutoMapper;
using Extension.Application.AppFactory;
using Extension.Application.Dto.Base;
using Extension.Application.Helper;
using Extension.Domain.Common;
using Extension.Domain.Infrastructure;
using Extension.Domain.Repositories;
using Extension.Services;
using Extension.Web.Controllers.Base;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Extension.Web.Controllers
{
    public class CrudBaseController<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> : BaseController
        where TEntity : class
        where TEntityDto : class
        where TPagedInput : PagedFullInputDto
        where TPagedOutput : class//, IEntity<TPrimaryKey>
    {
        protected readonly CrudBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput> _baseService;
        public readonly IAppFactory Factory;
        public readonly IBaseRepository<TPrimaryKey, TEntity> Repository;
        public readonly IMapper ObjectMapper;

        public CrudBaseController(IAppFactory factory)
        {
            _baseService = new CrudBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput>(factory);
            Factory = _baseService.Factory;
            Repository = _baseService.Repository;
            ObjectMapper = _baseService.ObjectMapper;
        }

        [HttpPost("List")] // Adding a route to the action method
        public virtual async Task<PagedResultDto<TPagedOutput>> GetPagingList(TPagedInput input)
        {
            try
            {
                var query = await this.GetQueryPagingLinq(input);

                return await query.GetPagedAsync(input);
            }
            catch (Exception ex)
            {
                return new PagedResultDto<TPagedOutput>(ex.Message);
            }
        }

        [NonAction]
        protected virtual async Task<IQueryable<TPagedOutput>> GetQueryPagingLinq(TPagedInput input)
        {
            var repo = Factory.Repository<TPrimaryKey, TEntity>()
            .GetAll();
            return repo.Select(item => Factory.ObjectMapper.Map<TPagedOutput>(item));
        }

        [HttpGet("GetById/{id}")]
        public virtual async Task<CommonResultDto<TEntityDto>> GetById(TPrimaryKey id)
        {
            return await _baseService.GetByIdAsync(id);
        }

        [HttpPost("Create")]
        public virtual async Task<CommonResultDto<TEntityDto>> Create(TEntityDto input)
        {
            return await _baseService.InsertAsync(input);
        }

        [HttpPut("Update")]
        public virtual async Task<CommonResultDto<TEntityDto>> Update(TEntityDto input)
        {
            return await _baseService.UpdateAsync(input);
        }

        [HttpDelete("DeleteById/{id}")]
        public virtual async Task<CommonResultDto<TEntityDto>> DeleteById(TPrimaryKey id)
        {
            return await _baseService.DeleteById(id);
        }
    }
}
