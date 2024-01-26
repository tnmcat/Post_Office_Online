using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using PostOffice.API.Data.Enums;
using PostOffice.API.DTOs.MoneyOrder;
using PostOffice.API.DTOs.MoneyScope;
using PostOffice.API.DTOs.MoneyServicePrice;
using PostOffice.API.DTOs.Pincode;
using System.Security.Claims;

namespace PostOffice.Client.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles = "customer")]
    public class MoneyOrderController : Controller
    {
        HttpClient httpClient = new HttpClient();
        private readonly string _viewPath = "../Areas/Client/View/MoneyOrder";
        private readonly string moneyScopeURL = "https://localhost:7053/api/MoneyScope/";
        private readonly string pincodeURL = "https://localhost:7053/api/Pincode/";
        private readonly string moneyorderURL = "https://localhost:7053/api/MoneyOrder/";
        private readonly string moneyserviceURL = "https://localhost:7053/api/MoneyService/";

        [HttpGet]   
        public async Task<IActionResult> Index()

        {
            ViewData["UserId"] = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewData["StreetAddress"] = User.FindFirst(ClaimTypes.StreetAddress)?.Value;
            ViewData["Email"] = User.FindFirst(ClaimTypes.Email).Value;
            ViewData["PhoneNumber"] = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
            ViewData["LastName"] = User.FindFirst(ClaimTypes.Name)?.Value;
            ViewData["FirstName"] = User.FindFirst(ClaimTypes.GivenName)?.Value;
            return View();
        }
        
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewData["UserId"] = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            ViewData["StreetAddress"] = User.FindFirst(ClaimTypes.StreetAddress)?.Value;
            ViewData["Email"] = User.FindFirst(ClaimTypes.Email).Value;
            ViewData["PhoneNumber"] = User.FindFirst(ClaimTypes.MobilePhone)?.Value;
            ViewData["LastName"] = User.FindFirst(ClaimTypes.Name)?.Value;
            ViewData["FirstName"] = User.FindFirst(ClaimTypes.GivenName)?.Value;

            List<PincodeBaseDTO>? pincodeList = JsonConvert.DeserializeObject<List<PincodeBaseDTO>>(
                    httpClient.GetStringAsync(pincodeURL + "PincodeList").Result
                );
            ViewBag.PincodeList = pincodeList;
            string json = await httpClient.GetStringAsync(pincodeURL + "PincodeList");

            return View();
        }
        [HttpPost]
        public IActionResult CreateMoneyOrder(MoneyOrderCreateDTO moneyOrderCreateDTO)
        {
            moneyOrderCreateDTO.user_id = new Guid(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
            moneyOrderCreateDTO.sender_name = User.FindFirst(ClaimTypes.Name)?.Value + " " +User.FindFirst(ClaimTypes.GivenName)?.Value;
            moneyOrderCreateDTO.sender_email = User.FindFirst(ClaimTypes.Email).Value;
            moneyOrderCreateDTO.sender_address = User.FindFirst(ClaimTypes.StreetAddress)?.Value;
            moneyOrderCreateDTO.sender_phone = User.FindFirst(ClaimTypes.MobilePhone)?.Value;

            moneyOrderCreateDTO.send_date = DateTime.Now;
            moneyOrderCreateDTO.receive_date = DateTime.Now.AddDays(3);
            moneyOrderCreateDTO.transfer_status = API.Data.Enums.TransferStatus.Pending;

            var test = httpClient.PostAsJsonAsync<MoneyOrderCreateDTO>(moneyorderURL, moneyOrderCreateDTO).Result;
            return Json(new { });
        }
        //caculate moneyscope and zonetype dbo.moneyservice
        [HttpPost]
        public async Task<IActionResult> ScopeFilter(float transfer_value, string sendPin, string recPin)
        {
            int zone_id;
            float total_charge;
            if (sendPin == recPin && sendPin != null && recPin != null)
            {
                zone_id = 1;
            }
            else
            {
                int send_area = JsonConvert.DeserializeObject<PincodeBaseDTO>(httpClient.GetStringAsync(pincodeURL + "PincodeById?id=" + sendPin).Result)!.area_id;
                int rec_area = JsonConvert.DeserializeObject<PincodeBaseDTO>(httpClient.GetStringAsync(pincodeURL + "PincodeById?id=" + recPin).Result)!.area_id;
                if (send_area != rec_area)
                {
                    zone_id = 3;
                }
                else
                {
                    zone_id = 2;
                }
            }
            var temp = await httpClient.GetStringAsync(moneyScopeURL + "ScopeValue" + "?value=" + transfer_value.ToString());
            MoneyScopeBaseDTO? moneyscope = JsonConvert.DeserializeObject<MoneyScopeBaseDTO>(temp);
            if (moneyscope == null)
            {
                return Json(new
                {
                    transfer_value = transfer_value,
                });
            }

            temp = httpClient.GetStringAsync(moneyserviceURL + "ZoneNScope" + "?zone=" + zone_id.ToString() + "&scope=" + moneyscope.id.ToString()).Result;
            MServicePriceBaseDTO? mServicePriceBaseDTO = JsonConvert.DeserializeObject<MServicePriceBaseDTO>(temp);

            total_charge = transfer_value + mServicePriceBaseDTO.fee;
            return Json(new
            {
                order_fee = mServicePriceBaseDTO.fee,
                description = moneyscope.description,
                total_charge = total_charge,

            });
        }

        public IActionResult submit(string successful)
        {
            return View();
        }
    }
}