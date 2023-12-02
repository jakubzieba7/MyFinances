using Azure;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace MyFinances.Models.Response
{
    public class PagedResponse<T> : Response
    {
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public Uri FirstPage { get; set; }
        public Uri LastPage { get; set; }
        public int TotalPages { get; set; }
        public int TotalRecords { get; set; }
        public Uri NextPage { get; set; }
        public Uri PreviousPage { get; set; }
        public T Data { get; set; }
        public PagedResponse(T data, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            Data = data;
        }

        public PagedResponse()
        {

        }
    }
}
