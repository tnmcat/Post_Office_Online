using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.ParcelService;

namespace PostOffice.Client.Areas.Client.ViewComponents
{
    public class ParcelServiceViewComponent:ViewComponent
    {
        private readonly string serviceURL = "https://localhost:7053/api/ParcelService/";
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        public ParcelServiceViewComponent()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IViewComponentResult Invoke()
        {
            List<ParcelServiceBaseDTO>? serviceList = JsonConvert.DeserializeObject<List<ParcelServiceBaseDTO>>(
                               _httpClient.GetStringAsync(serviceURL + "GetAllService").Result
                           );

            return View(serviceList);
        }
    }
}
