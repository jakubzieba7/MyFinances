namespace MyFinances.Models.Response
{
    public class DataResponse<T>:Response
    {
        public T Data { get; set; }
    }
}
