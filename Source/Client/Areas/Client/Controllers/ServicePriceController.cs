
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.ParcelServicePrice;
using System.Net.Http;

namespace PostOffice.Client.Areas.Client.Controllers
{
    [Area("Client")]
    [Authorize(Roles ="customer")]
    public class ServicePriceController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;

        public ServicePriceController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public async Task<IActionResult> Index()
        {
            List<ServicePriceExpress> servicePrice = new List<ServicePriceExpress>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/ParcelServicePrice/GetServiceExpress/Express");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    servicePrice = JsonConvert.DeserializeObject<List<ServicePriceExpress>>(data);
                }
                else
                {
                    throw new Exception("Error Message");
                }
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log error, set ViewBag message, etc.
            }
            return View(servicePrice);
        }
        [HttpGet]
        public async Task<IActionResult> IndexEconomy()
        {
            List<ServicePriceEconomy> servicePrice = new List<ServicePriceEconomy>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/ParcelServicePrice/GetServiceEconomy/Economy");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    servicePrice = JsonConvert.DeserializeObject<List<ServicePriceEconomy>>(data);
                }
                else
                {
                    throw new Exception("Error Message");
                }
            }
            catch (Exception ex)
            {
                // Handle exception, e.g., log error, set ViewBag message, etc.
            }
            return View(servicePrice);
        }
    }
}
