using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.Area;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreasController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public AreasController(AppDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // GET: api/Areas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaBaseDTO>>> GetAreas()
        {
          if (_context.Areas == null)
          {
              return NotFound();
          }
            var areas = await _context.Areas.ToListAsync();
            var records = _mapper.Map<List<AreaBaseDTO>>(areas);
            return Ok(records);
        }

        // GET: api/Areas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Area>> GetArea(int id)
        {
          if (_context.Areas == null)
          {
              return NotFound();
          }
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                return NotFound();
            }
            return area;
        }

        // PUT: api/Areas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArea(int id,AreaUpdateDTO areaUpdateDTO)
        {
            var area = await _context.Areas.FindAsync(id);

            if (area == null)
            {
                throw new Exception($"Area ID {id} is not found.");
            }

            _mapper.Map(areaUpdateDTO, area);

            var result = _context.Areas.Update(area);

            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Areas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<IActionResult> PostArea(AreaCreateDTO areaCreateDTO)
        {
         

            var newArea = _mapper.Map<Area>(areaCreateDTO);
            _context.Areas.Add(newArea);
            await _context.SaveChangesAsync();
            return Created($"/{newArea.id}", newArea);


        }

        // DELETE: api/Areas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArea(int id)
        {
            if (_context.Areas == null)
            {
                return NotFound();
            }
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }

            _context.Areas.Remove(area);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaExists(int id)
        {
            return (_context.Areas?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
