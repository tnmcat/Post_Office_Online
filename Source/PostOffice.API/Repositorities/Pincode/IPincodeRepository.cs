
using PostOffice.API.DTOs.MoneyOrder;

using PostOffice.API.DTOs.Pincode;

namespace PostOffice.API.Repositorities.Pincode
{
    using PostOffice.API.Data.Models;
    public interface IPincodeRepository
    {

        Task<PincodeBaseDTO> GetPincodeById(string id);
        Task<List<PincodeBaseDTO>> GetPincodes();
    }
}
