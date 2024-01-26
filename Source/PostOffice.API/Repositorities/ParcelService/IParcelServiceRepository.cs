
namespace PostOffice.API.Repositories.ParcelService
{
    using PostOffice.API.DTOs.ParcelService;
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.ParcelServicePrice;

    public interface IParcelServiceRepository
    {
        Task<ParcelService> AddParcelService(ParcelServiceCreateDTO parcelServiceCreateDTO);
        Task<bool> UpdateParcelService(int id, ParcelServiceUpdateDTO parcelServiceUpdateDTO);
       
        Task<ParcelServiceBaseDTO> GetParcelServiceById(int id);
        
    }
}
