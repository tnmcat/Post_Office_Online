using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.ParcelOrder;
using System.Data;
using System.Net.Http;
using System.Text;

namespace PostOffice.Admin.Controllers
{
    [Authorize(Roles ="admin")]
    public class ParcelOrderController : Controller
    {
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;

        public ParcelOrderController()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<ParcelOrderBase> parcelOrders = new List<ParcelOrderBase>();
            try
            {
                HttpResponseMessage response = await _httpClient.GetAsync(_httpClient.BaseAddress + "/ParcelOrder/GetAllOrders");
                if (response.IsSuccessStatusCode)
                {
                    string data = await response.Content.ReadAsStringAsync();
                    parcelOrders = JsonConvert.DeserializeObject<List<ParcelOrderBase>>(data);
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
            return View(parcelOrders);
        }
        [HttpGet("id")]
        public IActionResult Edit(int id)
        {
            try
            {
                ParcelOrderUpdateDTO parcelOrder = new ParcelOrderUpdateDTO();
                HttpResponseMessage response = _httpClient.GetAsync(_httpClient.BaseAddress + "/ParcelOrder/GetParcelOrderById/" + id).Result;

                if (response.IsSuccessStatusCode)
                {
                    string data = response.Content.ReadAsStringAsync().Result;
                    parcelOrder = JsonConvert.DeserializeObject<ParcelOrderUpdateDTO>(data);

                }
                return View(parcelOrder);
            }
            catch(Exception ex) 
            {
                
                return View();
            }
        }

        [HttpPost] 
        public async Task<IActionResult> Edit(int id, ParcelOrderUpdateDTO parcelOrderUpdate) 
        {
            string data = JsonConvert.SerializeObject(parcelOrderUpdate);
            StringContent content = new StringContent(data, Encoding.UTF8,"application/json");
            HttpResponseMessage response = await _httpClient.PutAsync(_httpClient.BaseAddress + "/ParcelOrder/UpdateParcelOrder/{id}", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index","ParcelOrder");
            }
            return View("Edit");
        }
    }
}
