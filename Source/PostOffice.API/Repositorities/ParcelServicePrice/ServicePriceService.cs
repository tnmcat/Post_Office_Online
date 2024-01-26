using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;

namespace PostOffice.API.Repositories.ParcelServciePrice
{
    using AutoMapper;
    using Microsoft.AspNetCore.Mvc;
    using PostOffice.API.Data.Context;
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.MoneyServicePrice;
    using PostOffice.API.DTOs.ParcelService;
    using PostOffice.API.DTOs.ParcelServicePrice;
    using PostOffice.API.Repositories.ParcelServicePrice;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ServicePriceService : IServicePriceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ServicePriceService(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParcelServicePrice> AddServicePrice(ServicePriceCreateDTO servicePriceCreateDTO)
        {
            var servicePriceEntity = _mapper.Map<ParcelServicePrice>(servicePriceCreateDTO);

            // Thêm dữ liệu vào cơ sở dữ liệu
            _context.ServicePrices.Add(servicePriceEntity);
            await _context.SaveChangesAsync();
            return servicePriceEntity;
        }

        public async Task<ServicePriceBaseDTO> GetByZone(int zone, int scope, int service, int parcelType)
        {
            var servicePrice = await _context.ServicePrices.Where(m => m.zone_type_id == zone && m.scope_weight_id == scope && m.service_id == service && m.parcel_type_id == parcelType)
              .FirstOrDefaultAsync();
            return _mapper.Map<ServicePriceBaseDTO>(servicePrice);
        }

        public async Task<ServicePriceBaseDTO> GetServicePriceById(int id)
        {
            var servicePriceId = _context.ServicePrices.SingleOrDefault(p => p.parcel_type_id == id);
            var servicePriceDto = _mapper.Map<ServicePriceBaseDTO>(servicePriceId);
            if (servicePriceDto == null)
            {
                throw new KeyNotFoundException();
            }
            return servicePriceDto;
        }
        public async Task<bool> UpdateServicePrice(int id, ServicePriceUpdateDTO servicePriceUpdateDTO)
        {
            var fee = await _context.ServicePrices.SingleOrDefaultAsync(p => p.parcel_price_id == id);

            if (fee == null)
            {
                return false;
            }

            // Update the properties of the product with the values from the DTO
            _mapper.Map(servicePriceUpdateDTO, fee);

            await _context.SaveChangesAsync();

            return true;
        }

    }
    
}
