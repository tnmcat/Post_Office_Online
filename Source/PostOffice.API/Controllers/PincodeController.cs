using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PostOffice.API.Repositorities.Pincode;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using System.Security.Policy;
using PostOffice.API.DTOs.Pincode;
using System.Net.Http;
using System.Security.Policy;


namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PincodeController : ControllerBase
    {
        private readonly IPincodeRepository _repository;
        public PincodeController(IPincodeRepository repository)
        {
            _repository = repository;
        }
        [HttpGet("PincodeById", Name= "GetPincodebyId")]
        public async Task<IActionResult> GetPincodeById(string id)
        {
            var pincodeDto = await _repository.GetPincodeById(id);
            if (pincodeDto == null)
            {
                return NotFound();
            }
            return Ok(pincodeDto);


        }

        [HttpGet("PincodeList")]
        public async Task<IActionResult> GetPincodes()
        {
            var pincodeDtos = await _repository.GetPincodes();
            if (pincodeDtos.Count() <= 0)
            {
                return NotFound();
            }
            return Ok(pincodeDtos);

        }



    }
}
