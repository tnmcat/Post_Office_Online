using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.DTOs.MoneyScope;

namespace PostOffice.API.Repositorities.MoneyScope
{
    using PostOffice.API.Data.Models;
    using System.Collections.Generic;

    public class MoneyScopeRepository : IMoneyScopeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public MoneyScopeRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<MoneyScope> CreateMoneyScope(MoneyScopeCreateDTO moneyScopeCreateDTO)
        {
            var moneyScope = _mapper.Map<MoneyScope>(moneyScopeCreateDTO);
            _context.MoneyScopes.AddAsync(moneyScope);
            await _context.SaveChangesAsync();
            return moneyScope;
        }

        public async Task<MoneyScopeBaseDTO> GetMoneyScopeById(int id)
        {
            var moneyScope = await _context.MoneyScopes.FindAsync(id);
            var moneyScopeDTO = _mapper.Map<MoneyScopeBaseDTO>(moneyScope);

            return moneyScopeDTO;
        }

        public async Task<MoneyScopeBaseDTO> GetMoneyScopeByValue(float value)
        {
            
            var moneyScope = await _context.MoneyScopes.Where(m => m.min_value <= value && m.max_value >= value).FirstOrDefaultAsync();
            var moneyScopeDTO = _mapper.Map<MoneyScopeBaseDTO>(moneyScope);
            return moneyScopeDTO;
        }

        public async Task<List<MoneyScopeBaseDTO>> ListMoneyScope()
        {
           var moneyscopelist = await _context.MoneyScopes.ToListAsync();
            var moneyscopelistDtos = _mapper.Map<List<MoneyScopeBaseDTO>>(moneyscopelist);
           
            return moneyscopelistDtos;
        }

        public  async Task<bool> UpdateMoneyScope(int id, MoneyScopeUpdateDTO moneyScopeUpdateDTO)
        {
            var moneyscopes = _context.MoneyScopes.SingleOrDefault(p => p.id == id);
            if (moneyscopes == null)
            {
                return false;
            }
            _mapper.Map(moneyscopes, moneyScopeUpdateDTO);
            _context.SaveChanges();

            return true;
        }
    }
}
