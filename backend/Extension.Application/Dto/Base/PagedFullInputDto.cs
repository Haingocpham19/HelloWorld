using Extension.Application.Utilities;
using Extension.Domain.CommanConstant;

namespace Extension.Application.Dto.Base
{
    public class PagedAndSortedInputDto : PagedInputDto
    {
        public string Sorting { get; set; }
        public PagedAndSortedInputDto()
        {
            MaxResultCount = AppConsts.DefaultPageSize;
        }
    }

    public class PagedFullInputDto : PagedAndSortedInputDto
    {
        public List<FilterColumns> ArrFilter { get; set; }

        public bool? IsFullRecord { get; set; }
        public string Filter { get; set; }
        public string FilterFullText => $"%{Filter}%";
        public string MySqlFullTextSearch => string.IsNullOrEmpty(Filter) ? null : $"\"{Filter}\"";
        public void Format()
        {
            if (!string.IsNullOrEmpty(this.Filter))
            {
                this.Filter = this.Filter.ToLower().Trim().Replace("  ", " ");
                this.Filter = StringHelperUtility.ConvertToUnsign(this.Filter);
            }
        }
        public int? TenantId { get; set; }
    }

    public class FilterColumns
    {
        public string FieldName { get; set; }
        public string FieldDisplay { get; set; }
        public FilterType? FilterType { get; set; }
        public List<string> Values { get; set; }
    }
}
