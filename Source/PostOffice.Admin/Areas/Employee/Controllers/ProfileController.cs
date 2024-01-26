using Microsoft.AspNetCore.Mvc;
using PostOffice.Admin.Services;
using PostOffice.API.DTOs.User;
using System.Security.Claims;

namespace PostOffice.Admin.Areas.Employee.Controllers
{
    [Area("Employee")]
    public class ProfileController : Controller
    {
        private readonly IUserApiClient _userApiClient;
        private readonly IConfiguration _configuration;

        public ProfileController(IUserApiClient userApiClient,
            IConfiguration configuration)
        {
            _userApiClient = userApiClient;
            _configuration = configuration;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {

            var userId = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            var result = await _userApiClient.GetById(userId);

            return View(result.ResultObj);
        }


        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var result = await _userApiClient.GetById(id);
            if (result.IsSuccessed)
            {
                var user = result.ResultObj;
                var updateRequest = new UserUpdateDTO()
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    PhoneNumber = user.PhoneNumber,
                    UserName = user.UserName,
                    PincodeId = user.PincodeId,
                    Address = user.Address,
                    Id = id
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserUpdateDTO request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.UpdateUser(request.Id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfully";
                return RedirectToAction("Index");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
        [HttpGet]
        public async Task<IActionResult> ChangePassword()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ChangePassword(UserChangePasswordDTO request)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _userApiClient.ChangePassword(request);
            if (result.IsSuccessed)
            {
                TempData["result_change_pass"] = "Change Password successfully";
                return View();
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}
