using AutoMapper;


namespace PostOffice.API.Repositorities.MoneyOrder
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.Data.Context;
    using PostOffice.API.DTOs.MoneyOrder;
    using PostOffice.API.Repositories.MoneyOrder;
    using System.Collections.Generic;
    using Microsoft.EntityFrameworkCore;
    using PostOffice.API.Data.Enums;
  

    public class MoneyOrderRepository : IMoneyOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;
        public MoneyOrderRepository(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<bool> CreateMoneyOrder(MoneyOrderCreateDTO moneyOrderDto)
        {
            var moneyOrder = _mapper.Map<MoneyOrder>(moneyOrderDto);
            await _context.MoneyOrders.AddAsync(moneyOrder);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<MoneyOrderBaseDTO> GetMoneyOrderById(int id)
        {
            var moneyOrder = await _context.MoneyOrders.FindAsync(id);
            var moneyOrderDTO = _mapper.Map<MoneyOrderBaseDTO>(moneyOrder);

            return moneyOrderDTO;

        }


        public async Task<List<MoneyOrderBaseDTO>> MoneyOrders()
        {
            var moneyorders = await _context.MoneyOrders.ToListAsync();
            var moneyorderDTOs = _mapper.Map<List<MoneyOrderBaseDTO>>(moneyorders);
            return moneyorderDTOs;
        }

        public async Task<bool> UpdateMoneyOrder(MoneyOrderUpdateDTO moneyOrderUpdateDTO, bool isStatus = false)
        {

            MoneyOrder moneyorders = _context.MoneyOrders.SingleOrDefault(p => p.id == moneyOrderUpdateDTO.id);
            if (moneyorders == null)
            {
                return false;
            }

            if (isStatus == true)
            {
                moneyorders.transfer_status = moneyOrderUpdateDTO.transfer_status;
            }
            else
            {
                _mapper.Map(moneyOrderUpdateDTO, moneyorders);
            }

            _context.SaveChanges();
            return true;
        }

        public async Task<List<string>> GetStatus()
        {
            var enumValues = Enum.GetValues(typeof(TransferStatus));
            var enumValueDtoList = new List<string>();

            foreach (var value in enumValues)
            {

                enumValueDtoList.Add(value.ToString());
            }

            return enumValueDtoList;
        }




    }
}
