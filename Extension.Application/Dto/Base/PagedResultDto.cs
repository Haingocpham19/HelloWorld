namespace Extension.Application.Dto.Base
{
    public class PagedResultDto<T>
    {
        public PagedResultDto()
        {
            _items = new List<T>();
        }

        public PagedResultDto(string error)
        {
            _error = error;  
        }

        private string _error;

        private IReadOnlyList<T> _items;

        //
        // Summary:
        //     List of items.
        public IReadOnlyList<T> Items
        {
            get
            {
                return _items ?? (_items = new List<T>());
            }
            set
            {
                _items = value;
            }
        }

        public int TotalCount { get; set; }
    }
}
