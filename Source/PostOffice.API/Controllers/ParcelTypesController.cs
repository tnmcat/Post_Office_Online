using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.ParcelType;
using PostOffice.API.Repositories.ParcelType;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ParcelTypesController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IParcelTypeRepository _repository;
        private readonly IMapper _mapper;


        public ParcelTypesController(AppDbContext context, IParcelTypeRepository repository, IMapper mapper)
        {
            _context = context;
            _repository = repository;
            _mapper = mapper;
        }

        // GET: api/ParcelTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParcelTypeBaseDTO>>> GetParcelTypes()
        {
            if (_repository.GetAllParcelTypes == null)
            {
                return NotFound();
            }
            return await _repository.GetAllParcelTypes();
        }
        [HttpGet("id")]
        public async Task<IActionResult> GetParcelTypeById(int id) 
        {
            var typeDto = await _repository.GetParcelTypeById(id);
            if (typeDto == null)
            {
                return NotFound();
            }
            return Ok(typeDto);

        }


        // PUT: api/ParcelTypes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{typeid}")]
        public async Task<IActionResult> Update(int typeid, [FromBody] ParcelTypeUpdateDTO parcelTypeUpdate)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            parcelTypeUpdate.id = typeid;
            var affectedResult = await _repository.UpdateParcelType(parcelTypeUpdate);
            if (affectedResult == null)
            {
                return BadRequest();
            }
            return Ok();

        }

        // POST: api/ParcelTypes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ParcelType>> PostParcelType([FromBody] ParcelTypeCreateDTO parcelTypeCreate)
        {
            if (ModelState.IsValid)
            {
                await _repository.AddParcelType(parcelTypeCreate);
                return Ok();
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

            // DELETE: api/ParcelTypes/5
            [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteParcelType(int id)
        {
            if (_context.ParcelTypes == null)
            {
                return NotFound();
            }
            var parcelType = await _context.ParcelTypes.FindAsync(id);
            if (parcelType == null)
            {
                return NotFound();
            }

            _context.ParcelTypes.Remove(parcelType);
            await _context.SaveChangesAsync();

            return NoContent();
        }

    }
}
