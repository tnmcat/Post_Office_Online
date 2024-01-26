using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.ParcelOrder;
using PostOffice.API.DTOs.ParcelServicePrice;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;

namespace PostOffice.Admin.Services
{
    public class ServicePriceAPIAdmin : IServicePriceAPIClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;
        
        public ServicePriceAPIAdmin(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration) 
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }
        public async Task<ApiResult<ParcelServicePriceDTO>> GetById(int parcel_price_id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/ParcelServicePrice/GetById/{parcel_price_id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ParcelServicePriceDTO>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ParcelServicePriceDTO>>(body);
        }

        public async Task<ApiResult<bool>> UpdateServicePrice(int parcel_price_id, ServicePriceUpdateDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/ParcelServicePrice/Update/{parcel_price_id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
    }
}
