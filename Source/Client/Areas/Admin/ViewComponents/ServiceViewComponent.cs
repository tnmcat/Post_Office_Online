using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.ParcelService;

namespace PostOffice.Client.Areas.Admin.ViewComponents
{
    public class ServicePriceViewComponent:ViewComponent
    {
        private readonly string servicePriceURL = "https://localhost:7053/api/ParcelServicePrice/";
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        public ServicePriceViewComponent()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IViewComponentResult Invoke()
        {
            List<ParcelServiceUpdateDTO>? serviceList = JsonConvert.DeserializeObject<List<ParcelServiceUpdateDTO>>(
                               _httpClient.GetStringAsync(servicePriceURL + "GetParcelServices").Result
                           );

            return View(serviceList);
        }
    }
}
