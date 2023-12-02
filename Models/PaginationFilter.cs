namespace MyFinances.Models
{
    public class PaginationFilter
    {
        const int maxPageSize = 20;
        public int PageNumber { get; set; } = 1;
        private int _pageSize = 10;

        //Model bound complex types must not be abstract or value types and must have a parameterless constructor.
        public PaginationFilter()
        {
            PageNumber = 1;
            PageSize = 10;
        }
        public PaginationFilter(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageSize
        {
            get
            {
                return _pageSize;
            }
            set
            {
                _pageSize = (value > maxPageSize) ? maxPageSize : value;
            }
        }

    }
}
