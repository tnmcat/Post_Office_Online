using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.ParcelType;

namespace PostOffice.Client.Areas.Client.ViewComponents
{
    public class ParcelTypeViewComponent:ViewComponent
    {
        private readonly string parcelTypeURL = "https://localhost:7053/api/ParcelTypes/";
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        public ParcelTypeViewComponent()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IViewComponentResult Invoke()
        {
            List<ParcelTypeBaseDTO>? typeList = JsonConvert.DeserializeObject<List<ParcelTypeBaseDTO>>(
                               _httpClient.GetStringAsync(parcelTypeURL + "GetParcelTypes").Result
                           );

            return View(typeList);
        }
    }
}
