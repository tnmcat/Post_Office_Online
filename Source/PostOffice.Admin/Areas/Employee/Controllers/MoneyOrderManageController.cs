using Microsoft.AspNetCore.Mvc;

namespace PostOffice.Admin.Areas.Employee.Controllers
{
    public class MoneyOrderManageController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
