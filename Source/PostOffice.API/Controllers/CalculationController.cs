using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs;

using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.Pincode;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CalculationController : ControllerBase
    {
        private readonly AppDbContext _context;
        public CalculationController(AppDbContext context) 
        {
            _context = context;
        }
        [HttpPost("CalculationFee")]
        public IActionResult CalculationFee([FromBody] Calculation calculation)
        {
            var tax = 0.1;
            var sender_area = (from p in _context.Pincodes where p.pincode == calculation.sender_pincode select p.area_id).FirstOrDefault();
            var receiver_area = (from p in _context.Pincodes where p.pincode == calculation.receiver_pincode select p.area_id).FirstOrDefault();
            var zonetype = 0;
            if (calculation.sender_pincode == calculation.receiver_pincode) 
            {
                zonetype = 1;
            }
            else if(calculation.sender_pincode != calculation.receiver_pincode && sender_area == receiver_area)
            {
                zonetype = 2;
            }
            else 
            {
                zonetype = 3;
            }
            var parcelweightScope = (from p in _context.WeightScopes where p.min_weight < calculation.weight && p.max_weight > calculation.weight select p.id).FirstOrDefault();
            var finarecordweight = (from p in _context.WeightScopes orderby p.min_weight ascending select p.min_weight).LastOrDefault();
            var pricescopeweight = (from p in _context.ServicePrices
                                    join w in _context.WeightScopes on p.scope_weight_id equals w.id orderby p.service_price ascending
                                    where p.zone_type_id == zonetype && p.service_id == calculation.service_id && calculation.parcel_type_id == p.parcel_type_id
                                    select p.service_price).LastOrDefault();
            var price = (from p in _context.ServicePrices 
                        where p.service_id == calculation.service_id 
                           && p.scope_weight_id == parcelweightScope 
                           && p.parcel_type_id == calculation.parcel_type_id
                           && p.zone_type_id == zonetype
                        select p.service_price).FirstOrDefault();

            var overprice = ((calculation.weight - finarecordweight) * pricescopeweight)/1000;
            if (calculation.weight < finarecordweight)
            {
                return Ok(price);
            }
            else 
            {
                return Ok(overprice);
            }
        }
    }
}
