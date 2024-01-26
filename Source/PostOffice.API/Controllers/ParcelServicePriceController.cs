using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.Area;
using PostOffice.API.DTOs.MoneyServicePrice;
using PostOffice.API.DTOs.ParcelService;
using PostOffice.API.DTOs.ParcelServicePrice;
using PostOffice.API.DTOs.ParcelType;
using PostOffice.API.DTOs.WeightScope;

using PostOffice.API.Repositories.ParcelServicePrice;
using System.Security.Policy;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParcelServicePriceController : ControllerBase
    {
        private readonly IServicePriceRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ParcelServicePriceController(IServicePriceRepository repository, AppDbContext context, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ServicePriceBaseDTO>>> GetPrices()
        {
            if (_context.ServicePrices == null)
            {
                return NotFound();
            }
            var servicesPrices = await _context.ServicePrices.ToListAsync();
            var records = _mapper.Map<List<ServicePriceBaseDTO>>(servicesPrices);
            return Ok(records);
        }
        [HttpPost]
        public async Task<IActionResult> AddServicePrice([FromBody] ServicePriceCreateDTO servicePriceCreateDTO)
        {
            await _repository.AddServicePrice(servicePriceCreateDTO);
            return Ok(servicePriceCreateDTO);

        }
        [HttpGet("id")]
        public async Task<IActionResult> GetPriceById(int id)
        {
            var serviceById = await _repository.GetServicePriceById(id);
            return Ok(serviceById);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateServicePrice(int id, [FromBody] ServicePriceUpdateDTO servicePriceUpdateDTO)
        {
            var feeUpdated = await _repository.UpdateServicePrice(id, servicePriceUpdateDTO);

            if (!feeUpdated)
            {
                return NotFound();
            }

            return NoContent();

        }

        [HttpGet("Express")]
        public async Task<IActionResult> GetServiceExpress()
        {
            var prices = from p in _context.ServicePrices
                         join w in _context.WeightScopes on p.scope_weight_id equals w.id
                         join z in _context.ZoneTypes on p.zone_type_id equals z.id
                         join s in _context.ParcelServices on p.service_id equals s.service_id
                         where w.id == p.scope_weight_id && z.id == p.zone_type_id
                         orderby s.service_id == 1, z.zone_description
                         select new ServicePriceExpress
                         {
                             id = p.parcel_price_id,
                             Name = s.name,
                             ServicePrice = (decimal)p.service_price,
                             Description = w.description,
                             ZoneDescription = z.zone_description
                         };

            return Ok(await prices.ToListAsync());
        }
        [HttpGet("Economy")]
        public async Task<IActionResult> GetServiceEconomy()
        {
            var prices = from p in _context.ServicePrices
                         join w in _context.WeightScopes on p.scope_weight_id equals w.id
                         join z in _context.ZoneTypes on p.zone_type_id equals z.id
                         join s in _context.ParcelServices on p.service_id equals s.service_id
                         where w.id == p.scope_weight_id && z.id == p.zone_type_id
                         orderby s.service_id == 2, z.zone_description
                         select new ServicePriceEconomy
                         {
                             id = p.parcel_price_id,
                             weightScope = new WeightScopeBaseDTO { description = w.description },
                             ZoneType = z,
                             service_price = p.service_price
                         };

            return Ok(await prices.ToListAsync());
        }

        [HttpGet("Zone")]
        public async Task<ServicePriceBaseDTO> GetByZone(int zone, int scope, int service, int parcelType)
        {
            var servicePriceDto = await _repository.GetByZone(zone, scope, service, parcelType);
            return servicePriceDto;
        }
    }
}

