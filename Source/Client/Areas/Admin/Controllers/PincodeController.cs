using Microsoft.AspNetCore.Mvc;

namespace PostOffice.Client.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class PincodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
