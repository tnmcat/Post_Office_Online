using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.ParcelServicePrice;

namespace PostOffice.Admin.Services
{
    public interface IServicePriceAPIClient
    {
        Task<ApiResult<ParcelServicePriceDTO>> GetById(int parcel_price_id);
        Task<ApiResult<bool>> UpdateServicePrice(int parcel_price_id, ServicePriceUpdateDTO request);
    }
}
