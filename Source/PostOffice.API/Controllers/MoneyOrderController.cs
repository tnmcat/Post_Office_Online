using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.Data.Context;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.MoneyOrder;
using PostOffice.API.Repositories.MoneyOrder;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MoneyOrderController : ControllerBase

    {
        private readonly IMoneyOrderRepository _repository;
        public MoneyOrderController(IMoneyOrderRepository repository)
        {
            _repository = repository;
        }

        [HttpGet("moneyorders/{id}", Name = "GetMoneyOrderById")]
        public async Task<IActionResult> GetMoneyOrderById(int id)
        {
            var moneyOrderDto = await _repository.GetMoneyOrderById(id);
            if (moneyOrderDto == null)
            {
                return NotFound();
            }
            return Ok(moneyOrderDto);
        }

        [HttpGet("MoneyorderList", Name = "GetMoneyOrders")]
        public async Task<IActionResult> MoneyOrders()
        {
            var moneyorderDTOs = await _repository.MoneyOrders();
            if (moneyorderDTOs == null)
            {
                return NotFound();
            }
            return Ok(moneyorderDTOs);
        }

        [HttpGet("StatusList", Name = "GetTransferStatus")]
        public async Task<IActionResult> GetStatus()
        {
            var status = await _repository.GetStatus();
            if (status == null)
            {
                return NotFound();
            }
            return Ok(status);
        }

        [HttpPost]
        public async Task<bool> CreateMoneyOrder(MoneyOrderCreateDTO moneyOrderDto)
        {
            await _repository.CreateMoneyOrder(moneyOrderDto);
            return true;
        }



        [HttpPost("UpdateMoneyManage", Name = "UpdateStatus")]
        public async Task<bool> UpdateMoneyOrder(MoneyOrderUpdateDTO moneyOrderUpdateDTO, bool isStatus = false)
        {
            var isUpdated = await _repository.UpdateMoneyOrder(moneyOrderUpdateDTO, isStatus);
            if (!isUpdated)
            {

                return false;
            }
            return isUpdated;
        }



    }
}
