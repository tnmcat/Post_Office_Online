using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PostOffice.API.DTOs.User;
using System.Security.Claims;

namespace PostOffice.Client.Areas.Client.Controllers
{
    [Area("Client")]
    public class HomeController : Controller
    {
        [Authorize(Roles ="customer")]
        public IActionResult Index()
        {
            ViewData["UserId"] = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewData["UserFirstName"] = User.FindFirst(ClaimTypes.GivenName)?.Value;
            ViewData["UserLastName"] = User.FindFirst(ClaimTypes.Name)?.Value;              
            ViewData["UserPinCode"] = User.FindFirst(ClaimTypes.PostalCode)?.Value;
            ViewData["StreetAddress"] = User.FindFirst(ClaimTypes.StreetAddress)?.Value;
            ViewData["Email"] = User.FindFirst(ClaimTypes.Email)?.Value;
            ViewData["PhoneNumber"] = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
            ViewData["Role"] = User.FindFirst(ClaimTypes.Role)?.Value;
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToPage("/Home/Index");
        }       




    }
}
