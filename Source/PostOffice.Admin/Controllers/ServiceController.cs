using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.Data.Models;
using PostOffice.API.DTOs.ParcelService;
using PostOffice.Admin.Services;
using System.Data;
using System.Text;

namespace PostOffice.Admin.Controllers
{
    [Authorize(Roles = "admin")]
    public class ServiceController : Controller
    {
        HttpClient httpClient = new HttpClient();
        private readonly string servicerURL = "https://localhost:7053/api/ParcelService/";
        private readonly HttpClient _httpClient;
        private readonly IParcelServiceAPIAdmin _parcelServiceAPIAdmin;
        private readonly IConfiguration _configuration;
        public ServiceController(IParcelServiceAPIAdmin parcelServiceAPIAdmin, IConfiguration configuration)
        {
            _configuration = configuration;
            _parcelServiceAPIAdmin = parcelServiceAPIAdmin;
        }
        [HttpGet]
        public IActionResult Index()
        {
            List<ParcelServiceBaseDTO>? servicemanage = JsonConvert.DeserializeObject<List<ParcelServiceBaseDTO>>(
                                        httpClient.GetStringAsync(servicerURL + "GetAllService").Result);
            return View(servicemanage);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();

        }
        [HttpPost]
        public async Task<IActionResult> Create(ParcelServiceCreateDTO parcelServiceCreate)
        {
            string data = JsonConvert.SerializeObject(parcelServiceCreate);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync(_httpClient.BaseAddress + "/ParcelService/Add", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","Service");
            }
            return View();

        }
        
        [HttpGet]
        [Authorize(Roles ="admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var result = await _parcelServiceAPIAdmin.GetById(id);
            if (result != null &&  result.IsSuccessed)
            {
                var parcelService = result.ResultObj;
                var updateRequest = new ParcelServiceUpdateDTO()
                {
                    service_id = parcelService.service_id,
                    name = parcelService.name,
                    description = parcelService.description,
                    status = parcelService.status,
                    delivery_time = (int)parcelService.delivery_time
                };
                return View(updateRequest);
            }
            return RedirectToAction("Error");
        }
        [HttpPost]
        [Authorize(Roles = "admin")]
        public async Task<IActionResult> Edit(ParcelServiceUpdateDTO request, int id)
        {
            if (!ModelState.IsValid)
                return View();

            var result = await _parcelServiceAPIAdmin.UpdateParcelService(id, request);
            if (result.IsSuccessed)
            {
                TempData["result"] = "Update successfully";
                return RedirectToAction("Index","Service");
            }

            ModelState.AddModelError("", result.Message);
            return View(request);
        }
    }
}
