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
using PostOffice.API.DTOs.WeightScope;
using PostOffice.API.Repositorities.WeightScope;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

        public class WeightScopesController : ControllerBase
        {
            private readonly IWeightScopeRepository _repository;
            private readonly IMapper _mapper;
            private readonly AppDbContext _context;


        public WeightScopesController(IWeightScopeRepository repository, IMapper mapper, AppDbContext context)
            {
                _repository = repository;
                _mapper = mapper;
                _context = context;
            }

            // GET api/weightscopes
            [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var weightscope = await _repository.GetAllAsync();
                return Ok(weightscope);
            }
            catch (Exception ex)
            {
                return BadRequest("An error occurred while fetching products.");
            }
        }
        // GET api/weightscopes/{id}
        //[HttpGet("{id}")]
        //    public async Task<IActionResult> Get(int id)
        //    {
        //        var weightScope = await _repository.GetByIdAsync(id);
        //        if (weightScope == null)
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            return Ok(weightScope);
        //        }
        //    }

        // POST api/weightscopes
        [HttpPost]
            public async Task<IActionResult> Post([FromBody] WeightScopeCreateDTO weightScope)
            {
                if (ModelState.IsValid)
                {
                    await _repository.AddAsync(weightScope);
                    return Ok();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            // PUT api/weightscopes/{id}
            [HttpPut("{id}")]
            public async Task<IActionResult> Put(int id, [FromBody] WeightScopeUpdateDTO weightScopeUpdate)
            {
                if (ModelState.IsValid)
                {
                    if (id != weightScopeUpdate.id)
                    {
                        return BadRequest();
                    }
                    await _repository.UpdateAsync(weightScopeUpdate);
                    return NoContent();
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }

            // DELETE api/weightscopes/{id}
            [HttpDelete("{id}")]
            public async Task<IActionResult> Delete(int id)
            {
                await _repository.DeleteAsync(id);
                return NoContent();
            }

        [HttpGet("getWeightRange")]
        public async Task<ActionResult<WeightScope>> GetWeightRange(double weight)
        {
            var weightRange = await _context.WeightScopes.FirstOrDefaultAsync(w => w.min_weight <= weight && w.max_weight >= weight);

            if (weightRange == null)
            {
                return NotFound();
            }

            return weightRange;
        }
    }
    }
