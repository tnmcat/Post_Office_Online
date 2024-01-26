using Microsoft.EntityFrameworkCore;

using System.ComponentModel.DataAnnotations;
namespace PostOffice.API.Repositories.ParcelService
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.Data.Context;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System;
    using PostOffice.API.DTOs.ParcelService;
    using AutoMapper;
    using PostOffice.API.DTOs.ParcelServicePrice;

    public class ParcelServiceService : IParcelServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public ParcelServiceService(AppDbContext context, IMapper mapper) 
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ParcelService> AddParcelService(ParcelServiceCreateDTO parcelServiceCreateDTO)
        {
            var parcelService = _mapper.Map<ParcelServiceCreateDTO, ParcelService>(parcelServiceCreateDTO);
            _context.ParcelServices.Add(parcelService);
            await _context.SaveChangesAsync();
            return parcelService;
        }

        public async Task<bool> UpdateParcelService(int id,ParcelServiceUpdateDTO parcelServiceUpdateDTO)
        {
            var parcelservice = await _context.ParcelServices.SingleOrDefaultAsync(p => p.service_id == id);
            if (parcelservice == null)
            {
                return false;
            }
           _mapper.Map(parcelServiceUpdateDTO, parcelservice);

            _context.SaveChanges();

            return true;
        }

        public async Task<ParcelServiceBaseDTO> GetParcelServiceById(int id)
        {
            var parcelserviceByid = await _context.ParcelServices.SingleOrDefaultAsync(p => p.service_id == id);
            var parcelServiceDto = _mapper.Map<ParcelServiceBaseDTO>(parcelserviceByid);
            if (parcelServiceDto == null)
            {
                throw new KeyNotFoundException();
            }
            return parcelServiceDto;
        }


    }
}
