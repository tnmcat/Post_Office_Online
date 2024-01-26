using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Threading.Tasks;

namespace PostOffice.API.Repositories.WeightScope
{
    using AutoMapper;
    using PostOffice.API.Data.Context;
    using PostOffice.API.Data.Models;

    using PostOffice.API.DTOs.WeightScope;
    using PostOffice.API.Repositorities.WeightScope;
    public class WeightScopeService : IWeightScopeRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;


        public WeightScopeService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<WeightScopeBaseDTO>> GetAllAsync()
        {
            var weightscopes = await _context.WeightScopes.ToListAsync();

            return weightscopes.Select(p => new WeightScopeBaseDTO
            {
                min_weight=p.min_weight,
                max_weight=p.max_weight
            }).ToList();
        }

        public async Task<WeightScope> AddAsync(WeightScopeCreateDTO weightScope)
        {
            var weightscope = _mapper.Map<WeightScope>(weightScope);
            await _context.WeightScopes.AddAsync(weightscope);
            await _context.SaveChangesAsync();
            return weightscope;
        }

        public async Task<WeightScope> UpdateAsync(WeightScopeUpdateDTO weightScopeUpdate)
        {
            var weightscope = _context.WeightScopes.FirstOrDefault(p => p.id == weightScopeUpdate.id );
            if (weightscope != null)
            {
                // Assign updated values to the product entity
                weightscope.max_weight = weightScopeUpdate.max_weight;
                weightscope.min_weight = weightscope.min_weight;
                weightscope.description = weightscope.description;

                await _context.SaveChangesAsync();
            }

            return weightscope;

        }

        public async Task DeleteAsync(int id)
        {
            var weightScope = await _context.WeightScopes.FindAsync(id);
            if (weightScope != null)
            {
                _context.WeightScopes.Remove(weightScope);
                await _context.SaveChangesAsync();
            }
        }

        public Task<WeightScopeBaseDTO> GetByIdAsync(int Id)
        {
            throw new NotImplementedException();
        }

        List<WeightScopeBaseDTO> IWeightScopeRepository.GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<WeightScope> GetPriceWeight(int id)
        {
            throw new NotImplementedException();
        }
    }
}
