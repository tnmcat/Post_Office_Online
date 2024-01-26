   
namespace PostOffice.API.Repositories.ParcelOrder
{
 using PostOffice.API.DTOs.ParcelOrder;
 using PostOffice.API.Data.Models;
 using PostOffice.API.DTOs.Common;
 using PostOffice.API.DTOs.User;

    public interface IParcelOrderRepository
    {
        Task<ParcelOrder> AddParcelOrderAsync(ParcelOrderCreateDTO parcelOrderDTO);
        Task<bool> UpdateParcelOrder(int id, ParcelOrderUpdateDTO parcelOrderUpdateDTO);
        Task<ParcelOrderBase> GetParcelOrderById(int id);            
        Task<ParcelOrderFeeShippingDTO> GetOrderWithFee(int id, ParcelOrderFeeShippingDTO dto);
        Task<ApiResult<ParcelOrderViewDTO>> GetById(int id);
        Task<ApiResult<bool>> Update(int id, ParcelOrderUpdateDTO request);
        Task<ApiResult<PagedResult<ParcelOrderViewDTO>>> GetAllParcelOrderPaging(GetParcelOrderPagingRequest request);

    }
}
