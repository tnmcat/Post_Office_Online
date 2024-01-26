 using AutoMapper;
 using Microsoft.EntityFrameworkCore;
namespace PostOffice.API.Repositories.ParcelType
{
   
    using PostOffice.API.Data.Context;
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.MoneyOrder;
    using PostOffice.API.DTOs.ParcelService;
    using PostOffice.API.DTOs.ParcelType;
    using PostOffice.API.DTOs.Pincode;

    public class ParcelTypeService : IParcelTypeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ParcelTypeService(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParcelType> AddParcelType(ParcelTypeCreateDTO parcelTypeCreateDTO)
        {
            var parcelTypeEntity = _mapper.Map<ParcelType>(parcelTypeCreateDTO);

            // Thêm dữ liệu vào cơ sở dữ liệu
            _context.ParcelTypes.Add(parcelTypeEntity);
            await _context.SaveChangesAsync();
            return parcelTypeEntity;

        }

        public async Task<List<ParcelTypeBaseDTO>> GetAllParcelTypes()
        {
            var parcelTypes = await _context.ParcelTypes.ToListAsync();
            var parcelTypeDTOs = _mapper.Map<List<ParcelTypeBaseDTO>>(parcelTypes);
            return parcelTypeDTOs;
        }

        public async Task<ParcelTypeBaseDTO> GetParcelType(int id)
        {
            var parceltypeByid = await _context.ParcelTypes.FindAsync(id);
            var parcelTypeDto =  _mapper.Map<ParcelTypeBaseDTO>(parceltypeByid);
            if (parcelTypeDto == null)
            {
                throw new KeyNotFoundException();
            }
            return parcelTypeDto;
        }

        public async Task<ParcelTypeBaseDTO> GetParcelTypeById(int id)
        {
            var type = await _context.ParcelTypes.FindAsync(id);
            var typeDTO = _mapper.Map<ParcelTypeBaseDTO>(type);

            return typeDTO;
        }

        public async Task<ParcelType> UpdateParcelType(ParcelTypeUpdateDTO parcelTypeUpdateDTO)
        {
            var parceltype = _context.ParcelTypes.Find(parcelTypeUpdateDTO.id);
            if (parceltype != null)
            {
                // Assign updated values to the product entity
                parceltype.max_length= parcelTypeUpdateDTO.max_length;
                parceltype.max_width = parcelTypeUpdateDTO.max_width;
                parceltype.max_height = parcelTypeUpdateDTO.max_height;

                await _context.SaveChangesAsync();
            }

            return parceltype;
        }
    }
}
