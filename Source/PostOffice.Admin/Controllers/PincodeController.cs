using Microsoft.AspNetCore.Mvc;

namespace PostOffice.Admin.Controllers
{
    public class PincodeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
