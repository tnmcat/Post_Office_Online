
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.Pincode;
using System.Net.Http;

namespace PostOffice.Client.Areas.Client.ViewComponents
{
    public class PincodeViewComponent : ViewComponent
    {
        private readonly string pincodeURL = "https://localhost:7053/api/Pincode/";
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        public PincodeViewComponent()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IViewComponentResult Invoke()
        {

            List<PincodeBaseDTO>? pincodeList = JsonConvert.DeserializeObject<List<PincodeBaseDTO>>(
          _httpClient.GetStringAsync(pincodeURL + "PincodeList").Result
      );

            return View(pincodeList);
        }
    }
}
