using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.MoneyOrder;
using PostOffice.API.DTOs.Pincode;
using System.Security.Claims;
using System.Text.Json.Serialization;

namespace PostOffice.Client.Areas.Client.Controllers
{
    [Area("Client")]
    public class StatisticController : Controller
    {
        HttpClient httpClient = new HttpClient();

        private readonly string moneyorderURL = "https://localhost:7053/api/MoneyOrder/";
        private readonly string _viewPath = "../Areas/Client/View/Statistic";
        [HttpGet]
        public IActionResult Index()
        {

            List<MoneyOrderBaseDTO>? statistic = JsonConvert.DeserializeObject<List<MoneyOrderBaseDTO>>(
                             httpClient.GetStringAsync(moneyorderURL + "MoneyorderList").Result);
            
            statistic = statistic.Where(m => m.user_id == new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value)).ToList();
            return View("Statistic", statistic);

        }
        [HttpPost]
        public IActionResult Statistic(string option)
        {

            return View();
        }


    }
}
