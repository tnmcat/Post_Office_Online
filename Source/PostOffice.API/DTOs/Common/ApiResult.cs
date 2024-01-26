namespace PostOffice.API.DTOs.Common
{
    public class ApiResult<T>
    {

        public bool IsSuccessed { get; set; }

        public string Message { get; set; }

        public T ResultObj { get; set; }
    }
}
