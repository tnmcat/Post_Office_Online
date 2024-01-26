using PostOffice.API.Data.Models;
namespace PostOffice.API.Repositorities.MoneyServicePrice
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.MoneyServicePrice;
    
    public interface IMoneyServiceRepository
    {
        Task<MoneyServicePrice> GetMoneyServiceById(int id);

        Task<MoneyServicePrice> CreateMoneyServicePrice(MServicePriceCreateDTO mServicePriceCreateDTO);

        Task<bool> UpdateMoneyService(int id, MServicePriceUpdateDTO mServicePriceUpdateDTO);

        Task<MServicePriceBaseDTO>  GetByZoneNScope(int zone, int scope);

    }
}
