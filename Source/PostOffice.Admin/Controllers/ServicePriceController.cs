using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.Admin.Services;
using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.ParcelService;
using PostOffice.API.DTOs.ParcelServicePrice;
using System.Data;
using System.Net.Http;
using System.Text;

namespace PostOffice.Admin.Controllers
{
    public class ServicePriceController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        private readonly IServicePriceAPIClient _servicePriceApiClient;
        private readonly IConfiguration _configuration;

        public ServicePriceController(IServicePriceAPIClient servicePriceApiClient,
            IConfiguration configuration)
        {
            _servicePriceApiClient = servicePriceApiClient;
            _configuration = configuration;
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
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
        //[HttpGet]
        //public async Task<IActionResult> Edit(int id)
        //{
        //    try
        //    {
        //        ServicePriceUpdateDTO servicePrice = new ServicePriceUpdateDTO();
        //        HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ParcelServicePrice/GetPriceById/" + id).Result;
        //        if (response.IsSuccessStatusCode)
        //        {
        //            string data = response.Content.ReadAsStringAsync().Result;
        //            servicePrice = JsonConvert.DeserializeObject<ServicePriceUpdateDTO>(data);

        //        }
        //        return View(servicePrice);
        //    }
        //    catch (Exception ex)
        //    {

        //        return View();
        //    }
        //}
        //[HttpPost]
        //public IActionResult Edit(ServicePriceUpdateDTO servicePriceUpdate)
        //{
        //    string data = JsonConvert.SerializeObject(servicePriceUpdate);
        //    StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
        //    HttpResponseMessage response = _httpClient.PutAsync(_httpClient.BaseAddress + "/ParcelServicePrice/UpdateServicePrice", content).Result;
        //    if (response.IsSuccessStatusCode)
        //    {

        //        return RedirectToAction("Index", "ServicePrice");

        //    }
        //    return View();
        //}
        [HttpGet]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(int parcel_price_id)
        {
            var result = await _servicePriceApiClient.GetById(parcel_price_id);
            if (result.IsSuccessed)
            {
                var servicePriceDTO = result.ResultObj;
                var updateRequest = new ServicePriceUpdateDTO()
                {
                    parcel_price_id = servicePriceDTO.parcel_price_id,
                    service_price = servicePriceDTO.service_price

                };
                return View(updateRequest);
            }
            return RedirectToAction("Error", "Home");
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(ServicePriceUpdateDTO request, int parcel_price_id)
        {
            if (!ModelState.IsValid)
                return View();


            var result = await _servicePriceApiClient.UpdateServicePrice(parcel_price_id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfully";
                return RedirectToAction("Index", "ServicePrice");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}
