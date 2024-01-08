using Extension.Domain.CommanConstant;
using System.ComponentModel.DataAnnotations;

namespace Extension.Application.Dto.Base
{
    public class PagedInputDto
    {
        [Range(1, AppConsts.MaxPageSize)]
        public int MaxResultCount { get; set; }

        [Range(0, int.MaxValue)]
        public int SkipCount { get; set; }

        public PagedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }
}
