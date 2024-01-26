using Microsoft.AspNetCore.Mvc;

namespace PostOffice.API.Controllers
{
    public class EmployeeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
