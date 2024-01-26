using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PostOffice.API.DTOs.User;
using PostOffice.API.Data.Context;
using PostOffice.API.DTOs.Area;
using Microsoft.AspNetCore.Identity;
using PostOffice.API.Data.Models;
using Microsoft.AspNetCore.Authorization;

namespace PostOffice.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly RoleManager<AppRole> _roleManager;

        public UserController(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }



        // GET: api/User
        
        [HttpGet]
        [Authorize(Roles="admin")]
        public async Task<IActionResult> GetAllEmployee()
        {
            var employeeListView = new List<UserBaseDTO>();
            var employees = await _userManager.GetUsersInRoleAsync("employee");
            foreach (var user in employees)
            {                  
                var employee = new UserBaseDTO
                {
                    UserName = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    PhoneNumber = user.PhoneNumber,
                    Address = user.Address,
                    Create_date = user.Create_date,
                    Status = user.Status,                           
                };
                employeeListView.Add(employee);
            }              
            return Ok(employeeListView);
        }
    }          

        

     

     

    
}

