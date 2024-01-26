using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.ParcelService;

namespace PostOffice.Client.Services
{
    public interface IParcelServiceAPIAdmin
    {
        Task<ApiResult<ParcelServiceBaseDTO>> GetById(int id);
        Task<ApiResult<bool>> UpdateParcelService(int id, ParcelServiceUpdateDTO request);
    }
}
