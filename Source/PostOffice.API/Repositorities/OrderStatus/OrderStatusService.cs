using System.Threading.Tasks;
using System.Collections.Generic;
using System;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace PostOffice.API.Repositories.OrderStatus
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.Data.Context;
    using PostOffice.API.DTOs.OrderStatus;
    using Microsoft.AspNetCore.Mvc;
    using PostOffice.API.DTOs.Common;
    using Microsoft.AspNetCore.Identity;
    using PostOffice.API.DTOs.Pincode;

    public class OrderStatusService : IOrderStatusRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderStatusService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<List<OrderStatusBase>> GetStatus()
        {
            var status = await _context.OrderStatuss.ToListAsync();
            var statusDto = _mapper.Map<List<OrderStatusBase>>(status);
            return statusDto;
        }

        public async Task<OrderStatusBase> GetStatusById(int id)
        {
            var status = await _context.OrderStatuss.SingleOrDefaultAsync(p =>p.Id == id);
            var statusDTO = _mapper.Map<OrderStatusBase>(status);

            return statusDTO;
        }
    }
}
