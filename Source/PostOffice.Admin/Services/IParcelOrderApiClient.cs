using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.TrackHistory;
using PostOffice.API.DTOs.User;

namespace PostOffice.Admin.Services
{
    public interface IParcelOrderApiClient
    {
        Task<ApiResult<PagedResult<ParcelOrderViewDTO>>> GetAllParcelOrderPaging(GetUserPagingRequest request);
        Task<ApiResult<ParcelOrderViewDTO>> GetById(int id);
        Task<ApiResult<TrackHistoryViewDTO>> GetHistoryByOrderId(int id);
        Task<ApiResult<bool>> UpdateParcelOrder(int id, ParcelOrderUpdateDTO request);
        
    }
}
