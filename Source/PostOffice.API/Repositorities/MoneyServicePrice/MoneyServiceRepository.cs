using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Models;
namespace PostOffice.API.Repositorities.MoneyServicePrice
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.Data.Context;
    using PostOffice.API.DTOs.MoneyServicePrice;


    public class MoneyServiceRepository : IMoneyServiceRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public MoneyServiceRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<MoneyServicePrice> CreateMoneyServicePrice(MServicePriceCreateDTO mServicePriceCreateDTO)
        {
            var moneyService = _mapper.Map<MoneyServicePrice>(mServicePriceCreateDTO);
            _context.MoneyServices.AddAsync(moneyService);
            await _context.SaveChangesAsync();
            return moneyService;

        }

        public async Task<MServicePriceBaseDTO> GetByZoneNScope(int zone, int scope)
        {
           var moneyService = await _context.MoneyServices.Where(m => m.zone_type_id == zone && m.money_scope_id == scope)
                .FirstOrDefaultAsync();
            return _mapper.Map<MServicePriceBaseDTO>(moneyService);

        }

        public async Task<MoneyServicePrice> GetMoneyServiceById(int id)
        {
           var moneyService = await _context.MoneyServices.FindAsync(id);
            var moneyServiceDTO = _mapper.Map<Data.Models.MoneyServicePrice>(moneyService);

            return moneyServiceDTO;
        }

        public async Task<bool> UpdateMoneyService(int id, MServicePriceUpdateDTO mServicePriceUpdateDTO)
        {
            MoneyServicePrice moneyservices = _context.MoneyServices.SingleOrDefault(p => p.id == id);
            if (moneyservices == null)
            {
                return false;
            }
            _mapper.Map(moneyservices, mServicePriceUpdateDTO);
            _context.SaveChanges();

            return true;
        }


    }
}
