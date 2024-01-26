using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using PostOffice.API.DTOs.MoneyServicePrice;
using PostOffice.API.Repositorities.MoneyScope;
using PostOffice.API.Repositorities.MoneyServicePrice;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyServiceController : ControllerBase
    {
        private readonly IMoneyServiceRepository _repository;
        public MoneyServiceController(IMoneyServiceRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("ZoneNScope", Name ="GetByZoneNScope")]
        public async Task<MServicePriceBaseDTO> GetByZoneNScope( int zone, int scope)
        {
            var moneyServiceDto = await _repository.GetByZoneNScope(zone, scope);
            return moneyServiceDto;
        }

        [HttpPut("UpdateMoneyService", Name ="Update")]
        public async Task<bool> UpdateMoneyService(int id, MServicePriceUpdateDTO mServicePriceUpdateDTO)
        {
            var mServicePriceUpdate = await _repository.UpdateMoneyService(id, mServicePriceUpdateDTO);
            return mServicePriceUpdate;
        }

    }
}
