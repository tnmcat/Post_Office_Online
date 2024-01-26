    using System.Threading.Tasks;
    using System.Collections.Generic;
    using System;
    using AutoMapper;
using System.Linq;

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
namespace PostOffice.API.Repositories.ParcelOrder
{
    using PostOffice.API.Data.Models;
    using PostOffice.API.Data.Context;
    using PostOffice.API.DTOs.ParcelOrder;
    using Microsoft.AspNetCore.Mvc;
    using PostOffice.API.DTOs.Common;
    using Microsoft.AspNetCore.Identity;
    using PostOffice.API.DTOs.User;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using System.Drawing.Printing;
    using Microsoft.CodeAnalysis;
    using PostOffice.API.Data.Enums;

    public class ParcelOrderService : IParcelOrderRepository
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ParcelOrderService(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ParcelOrder> AddParcelOrderAsync(ParcelOrderCreateDTO parcelOrderDTO)
        {
            var parcelOrder = _mapper.Map<ParcelOrder>(parcelOrderDTO);

            await _context.ParcelOrders.AddAsync(parcelOrder);
            await _context.SaveChangesAsync();
            return parcelOrder;
        }


    public async Task<ParcelOrderBase> GetParcelOrderById(int id)
        {
                var parcelOrder = await _context.ParcelOrders.FindAsync(id);
                var parcelOrderDto = _mapper.Map<ParcelOrderBase>(parcelOrder);

                return parcelOrderDto;

        }

        public async Task<bool> UpdateParcelOrder(int id, ParcelOrderUpdateDTO parcelOrderUpdateDTO)
        {
            var parcelorders = _context.ParcelOrders.SingleOrDefault(p => p.id == id);

            if (parcelorders == null)
            {
                return false;
            }
             _mapper.Map(parcelOrderUpdateDTO, parcelorders);

             _context.SaveChanges();

            return true;
        }    


        public async Task<ApiResult<PagedResult<ParcelOrderViewDTO>>> GetAllParcelOrderPaging(GetParcelOrderPagingRequest request)
        {
            var query = from p in _context.ParcelOrders select p;
            if (!string.IsNullOrEmpty(request.Keyword))
            {
                query = query.Where(order =>
                    order.description.Contains(request.Keyword)
                    || order.receiver_name.Contains(request.Keyword)
                    || order.receiver_address.Contains(request.Keyword)
                    || order.sender_name.Contains(request.Keyword)
                    || order.sender_email.Contains(request.Keyword)
                );
            }
            //3. Paging
            int totalRow = await query.CountAsync();
            var data = await query.Skip((request.PageIndex - 1) * request.PageSize).Take(request.PageSize).Select(p => new ParcelOrderViewDTO()
            {
                id = p.id,
                sender_name = p.sender_name,
                sender_pincode = p.sender_pincode,
                sender_address = p.sender_address,
                sender_phone = p.sender_phone,
                sender_email = p.sender_email,

                receiver_name = p.receiver_name,
                receiver_pincode = p.receiver_pincode,
                receiver_address = p.receiver_address,
                receiver_phone = p.receiver_phone,
                receiver_email = p.receiver_email,

                order_status = p.order_status,
                description = p.description,
                note = p.note,
                parcel_length = p.parcel_length,
                parcel_height = p.parcel_height,
                parcel_width = p.parcel_width,
                parcel_weight = p.parcel_weight,

                payer = p.payer,
                payment_method = p.payment_method,

                send_date = p.send_date,
                receive_date = p.receive_date,
                vpp_value = p.vpp_value,
                total_charge = p.total_charge               
            }).ToListAsync();

            //4. Select and projection
            var pagedResult = new PagedResult<ParcelOrderViewDTO>()
            {
                PageIndex = request.PageIndex,
                PageSize = request.PageSize,
                TotalRecords = totalRow,
                Items = data
            };
            return new ApiSuccessResult<PagedResult<ParcelOrderViewDTO>>(pagedResult);
        }
        public async Task<ApiResult<ParcelOrderViewDTO>> GetById(int id)
        {
            var parcelorder = await _context.ParcelOrders.SingleOrDefaultAsync(p => p.id == id);
            if (parcelorder == null)
            {
                return new ApiErrorResult<ParcelOrderViewDTO>("Order is not exist");
            }

            var parcelOrderView = new ParcelOrderViewDTO()
            {
                user_id = parcelorder.user_id,
                id = parcelorder.id,
                sender_name = parcelorder.sender_name,
                sender_pincode = parcelorder.sender_pincode,
                sender_address = parcelorder.sender_address,
                sender_phone = parcelorder.sender_phone,
                sender_email = parcelorder.sender_email,

                receiver_name = parcelorder.receiver_name,
                receiver_pincode = parcelorder.receiver_pincode,
                receiver_address = parcelorder.receiver_address,
                receiver_phone = parcelorder.receiver_phone,
                receiver_email = parcelorder.receiver_email,

                order_status = parcelorder.order_status,
                description = parcelorder.description,
                note = parcelorder.note,
                parcel_length = parcelorder.parcel_length,
                parcel_height = parcelorder.parcel_height,
                parcel_width = parcelorder.parcel_width,
                parcel_weight = parcelorder.parcel_weight,

                payer = parcelorder.payer,
                payment_method = parcelorder.payment_method,

                send_date = parcelorder.send_date,
                receive_date = parcelorder.receive_date,

                vpp_value = parcelorder.vpp_value,
                total_charge = parcelorder.total_charge                
            };
            return new ApiSuccessResult<ParcelOrderViewDTO>(parcelOrderView);
        }

        public async Task<ApiResult<bool>> Update(int id, ParcelOrderUpdateDTO request)
        {
            var parcelorder = await _context.ParcelOrders.FindAsync(id);
            if (parcelorder == null)
            {
                return new ApiErrorResult<bool>("Order is not exist");
            }           
            parcelorder.sender_name = request.sender_name;
            parcelorder.sender_pincode = request.sender_pincode;
            parcelorder.sender_address = request.sender_address;
            parcelorder.sender_phone = request.sender_phone;
            parcelorder.sender_email = request.sender_email;

            parcelorder.receiver_name = request.receiver_name;
            parcelorder.receiver_pincode = request.receiver_pincode;
            parcelorder.receiver_address = request.receiver_address;
            parcelorder.receiver_phone = request.receiver_phone;
            parcelorder.receiver_email = request.receiver_email;

            parcelorder.order_status = (int)request.order_status;
            parcelorder.description = request.description;
            parcelorder.note = request.note;
            parcelorder.parcel_length = (float)request.parcel_length;
            parcelorder.parcel_height = (float)request.parcel_height;
            parcelorder.parcel_width = (float)request.parcel_height;
            parcelorder.parcel_weight = (float)request.parcel_weight;

            parcelorder.payer = request.payer;
            parcelorder.payment_method = request.payment_method;

            parcelorder.send_date = request.send_date;
            parcelorder.receive_date = request.receive_date;

            parcelorder.vpp_value = (float)request.vpp_value;
            parcelorder.total_charge = (float)request.total_charge;

            bool result  = await _context.SaveChangesAsync() > 0;
            if (result)
            {
                return new ApiSuccessResult<bool>();
            }
            return new ApiErrorResult<bool>("Update failed");
        }

        public Task<ParcelOrderFeeShippingDTO> GetOrderWithFee(int id, ParcelOrderFeeShippingDTO dto)
        {
            throw new NotImplementedException();
        }
    }
    
}
