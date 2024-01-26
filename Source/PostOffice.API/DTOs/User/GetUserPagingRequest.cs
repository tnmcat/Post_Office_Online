using PostOffice.API.DTOs.Common;

namespace PostOffice.API.DTOs.User
{
    public class GetUserPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
