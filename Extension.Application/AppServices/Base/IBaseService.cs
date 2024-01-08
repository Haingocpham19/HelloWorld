using Extension.Application.Dto.Base;
using Extension.Domain.Common;

namespace Extension.Services
{
    public interface IBaseService<TPrimaryKey, TEntity, TEntityDto, TPagedInput, TPagedOutput>
        where TEntity : class
        where TEntityDto : class
        where TPagedInput : PagedFullInputDto
        where TPagedOutput : class//, IEntity<TPrimaryKey>
    {
        Task<CommonResultDto<TEntityDto>> InsertAsync(TEntityDto entity);
        Task<CommonResultDto<TEntityDto>> UpdateAsync(TEntityDto entity);
        Task<CommonResultDto<TPrimaryKey>> DeleteAsync(TPrimaryKey Id);
        Task<CommonResultDto<TEntityDto>> GetByIdAsync(TPrimaryKey Id);
    }
}
