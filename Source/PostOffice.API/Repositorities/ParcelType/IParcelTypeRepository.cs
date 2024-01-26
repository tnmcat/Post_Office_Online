
namespace PostOffice.API.Repositories.ParcelType
{
    using PostOffice.API.DTOs.ParcelType;
    using PostOffice.API.Data.Models;
    public interface IParcelTypeRepository
    {
        Task<ParcelType> AddParcelType(ParcelTypeCreateDTO parcelTypeCreateDTO);
        Task<ParcelType> UpdateParcelType(ParcelTypeUpdateDTO parcelTypeUpdateDTO);

        Task<ParcelTypeBaseDTO> GetParcelType(int id);
        Task<List<ParcelTypeBaseDTO>> GetAllParcelTypes();
        Task<ParcelTypeBaseDTO> GetParcelTypeById(int id);
    }
}
