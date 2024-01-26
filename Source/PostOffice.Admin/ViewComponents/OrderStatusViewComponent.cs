using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PostOffice.API.DTOs.OrderStatus;
using PostOffice.API.DTOs.ParcelService;

namespace PostOffice.Admin.ViewComponents
{
    public class OrderStatusViewComponent: ViewComponent
    {
        private readonly string servicePriceURL = "https://localhost:7053/api/OrderStatus/";
        Uri baseAddress = new Uri("https://localhost:7053/api");
        private readonly HttpClient _httpClient;
        public OrderStatusViewComponent()
        {
            _httpClient = new HttpClient();
            _httpClient.BaseAddress = baseAddress;
        }
        public IViewComponentResult Invoke()
        {
            List<OrderStatusBase>? serviceList = JsonConvert.DeserializeObject<List<OrderStatusBase>>(
                               _httpClient.GetStringAsync(servicePriceURL + "GetStatus").Result
                           );

            return View(serviceList);
        }
    }
}
