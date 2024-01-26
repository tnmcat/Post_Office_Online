namespace PostOffice.API.Repositories.ParcelServicePrice
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.MoneyServicePrice;
    using PostOffice.API.DTOs.ParcelService;
    using PostOffice.API.DTOs.ParcelServicePrice;

    public interface IServicePriceRepository
    {
        Task<ParcelServicePrice> AddServicePrice(ServicePriceCreateDTO servicePriceCreateDTO);
        Task<bool> UpdateServicePrice(int id, ServicePriceUpdateDTO servicePriceUpdateDto);
        Task<ServicePriceBaseDTO> GetServicePriceById(int id);
        Task<ServicePriceBaseDTO> GetByZone(int zone, int scope, int service, int parcelType);

    }
}
