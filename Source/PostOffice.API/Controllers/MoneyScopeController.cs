
using Microsoft.AspNetCore.Mvc;
using PostOffice.API.DTOs.MoneyScope;
using PostOffice.API.Repositorities.MoneyScope;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyScopeController : ControllerBase
    {
        private readonly IMoneyScopeRepository _repository;

        public MoneyScopeController(IMoneyScopeRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("moneyscope", Name = "GetMoneyScopeById")]

        public async Task<IActionResult> GetMoneyScopeById(int id) 
        { 
            var moneyScopeDto = await _repository.GetMoneyScopeById(id);
            if(moneyScopeDto == null)
            {
                return NotFound();
            }
            return Ok(moneyScopeDto);
        }

        [HttpPost]
        public async Task<IActionResult> CreateMoneyScope([FromBody] MoneyScopeCreateDTO moneyScopeDTO)
        {
            await _repository.CreateMoneyScope(moneyScopeDTO);
            return Ok(moneyScopeDTO);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMoneyScope(int id, [FromBody] MoneyScopeUpdateDTO moneyScopeUpdateDTO)
        {
            var isUpdated = await _repository.UpdateMoneyScope(id, moneyScopeUpdateDTO);
            if (!isUpdated)
            {

                return NotFound();
            }
            return NoContent();
        }

        [HttpGet("ScopeValue", Name = "GetMoneyScopeByValue")]
        public async Task<MoneyScopeBaseDTO> GetMoneyScopeByValue(float value)
        {
            var moneyScopeDto = await _repository.GetMoneyScopeByValue(value);
            return moneyScopeDto;
        }

        [HttpGet("ScopeList", Name ="GetScopeList")]
        public async Task<IActionResult> ListMoneyScope()
        {
            var moneyScopeDtos = await _repository.ListMoneyScope();
            if(moneyScopeDtos == null)
            {
                return NotFound();
            }
            return Ok(moneyScopeDtos);
        }
    }
}
