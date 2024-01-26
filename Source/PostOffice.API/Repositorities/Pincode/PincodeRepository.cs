using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.Area;
using PostOffice.API.DTOs.MoneyOrder;
using PostOffice.API.DTOs.Pincode;

namespace PostOffice.API.Repositorities.Pincode
{
    public class PincodeRepository : IPincodeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public PincodeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<PincodeBaseDTO> GetPincodeById(string id)
        {
            var pincode = await _context.Pincodes.FindAsync(id);
            var pincodeDTO = _mapper.Map<PincodeBaseDTO>(pincode);

            return pincodeDTO;
        }

        public async Task<List<PincodeBaseDTO>> GetPincodes()
        {
            var pincodes = await _context.Pincodes.Include(p => p.Area).ToListAsync();
            var pincodeDTOs = _mapper.Map<List<PincodeBaseDTO>>(pincodes);
            return pincodeDTOs;
        }
    }
}
