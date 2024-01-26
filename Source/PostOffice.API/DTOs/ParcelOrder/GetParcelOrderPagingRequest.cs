using PostOffice.API.DTOs.Common;

namespace PostOffice.API.DTOs.ParcelOrder
{
    public class GetParcelOrderPagingRequest : PagingRequestBase
    {
        public string Keyword { get; set; }
    }
}
