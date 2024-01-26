using Microsoft.AspNetCore.Mvc;

namespace PostOffice.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Edit()
        {
            return View();
        }
        public IActionResult Profile()
        {
            return View();
        }
    }
}
