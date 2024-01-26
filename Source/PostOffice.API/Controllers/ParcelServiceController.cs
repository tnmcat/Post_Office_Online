using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.Area;
using PostOffice.API.DTOs.ParcelService;
using PostOffice.API.Repositories.ParcelOrder;
using PostOffice.API.Repositories.ParcelService;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParcelServiceController : ControllerBase
    {
        private readonly IParcelServiceRepository _repository;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;
        public ParcelServiceController(IParcelServiceRepository repository, IMapper mapper, AppDbContext context)
        {
            _repository = repository;
            _mapper = mapper;
            _context = context;
        }

       
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelServiceBaseDTO>>> GetAllService()
        {
            if (_context.ParcelServices == null)
            {
                return NotFound();
            }
            var services = await _context.ParcelServices.ToListAsync();
            var records = _mapper.Map<List<ParcelServiceBaseDTO>>(services);
            return Ok(records);
        }

        [HttpGet("id")]
        public async Task<IActionResult> GetServiceById(int id) 
        {
            var serviceById = await _repository.GetParcelServiceById(id);
            return Ok(serviceById);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ParcelServiceCreateDTO parcelServiceCreateDto)
        {
            await _repository.AddParcelService(parcelServiceCreateDto);
            return Ok(parcelServiceCreateDto);

        }
        [HttpPut]
        public async Task<IActionResult> UpdateParcelService(int id, [FromBody] ParcelServiceUpdateDTO parcelServiceUpdateDTO)
        {
            var affectedResult = await _repository.UpdateParcelService(id, parcelServiceUpdateDTO);
            if (affectedResult == null)
            {
                return BadRequest();
            }
            return Ok();
        }
    }
}
