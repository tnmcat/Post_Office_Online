using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.DTOs.OrderStatus;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderStatusController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public OrderStatusController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<OrderStatusBase>>> GetStatus()
        {
            if (_context.OrderStatuss == null)
            {
                return NotFound();
            }
            var areas = await _context.OrderStatuss.ToListAsync();
            var records = _mapper.Map<List<OrderStatusBase>>(areas);
            return Ok(records);
        }
    }
}
