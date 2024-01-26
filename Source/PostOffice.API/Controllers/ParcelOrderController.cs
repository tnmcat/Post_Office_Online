using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace PostOffice.API.Controllers
{
    using AutoMapper;
    using PostOffice.API.Data.Context;
    using PostOffice.API.Data.Models;
    using PostOffice.API.DTOs.Area;
    using PostOffice.API.DTOs.ParcelOrder;
    using PostOffice.API.DTOs.ParcelServicePrice;
    using PostOffice.API.DTOs.User;
    using PostOffice.API.DTOs.WeightScope;
    using PostOffice.API.Repositories.ParcelOrder;
    using PostOffice.API.Repositories.ParcelService;
    using PostOffice.API.Repositories.ParcelType;

    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class ParcelOrderController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IParcelOrderRepository _repository;
        private readonly IMapper _mapper;
        public ParcelOrderController(IParcelOrderRepository repository, IMapper mapper, AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelOrderBase>>> GetAllOrders()
        {
            if (_context.ParcelOrders == null)
            {
                return NotFound();
            }
            var areas = await _context.ParcelOrders.ToListAsync();
            var records = _mapper.Map<List<ParcelOrderBase>>(areas);
            return Ok(records);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetParcelOrderById(int id)
        {
            var parcelOrderDto = await _repository.GetParcelOrderById(id);

            if (parcelOrderDto == null)
            {
                return NotFound();
            }

            return Ok(parcelOrderDto);
        }

        [HttpPost]
        public async Task<IActionResult> AddParcelOrder ([FromBody] ParcelOrderCreateDTO parcelOrderDto)
        {
            var parcelOrder = _mapper.Map<ParcelOrder>(parcelOrderDto);
            _context.ParcelOrders.Add(parcelOrder);
            await _context.SaveChangesAsync();
            return Ok(parcelOrder);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateParcelOrder(int orderid, [FromBody] ParcelOrderUpdateDTO parcelOrderUpdateDto) 
        {
            var isUpdated = await _repository.UpdateParcelOrder(orderid, parcelOrderUpdateDto);

            if (!isUpdated)
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpGet("paging")]
        public async Task<IActionResult> GetAll([FromQuery] GetParcelOrderPagingRequest request)
        {
            var parcelOrder = await _repository.GetAllParcelOrderPaging(request);
            return Ok(parcelOrder);
        }
                //[HttpGet]
        //public async IActionResult OrderDetails(int id, ParcelInfo parcel)
        //{
        //    var detail = from s in _context.ParcelOrders
        //                 join w in _context.OrderStatuss on s.order_status equals w.Id
        //                 join p in _context.ParcelServices on s.service_id equals p.service_id
        //                 join t in _context.ParcelTypes on s.parcel_type_id equals t.id
        //                 where s.id == id select new parcel(
        //                 id = s.id,
                             
                             
        //                     );
        //    return Ok(detail);
        //}


        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var parcelOrder = await _repository.GetById(id);
            return Ok(parcelOrder);
        }


        //PUT: http://localhost/api/ParcelOrder/id
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ParcelOrderUpdateDTO request)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _repository.Update(id, request);
            if (!result.IsSuccessed)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
