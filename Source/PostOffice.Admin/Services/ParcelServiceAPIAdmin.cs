using Newtonsoft.Json;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.User;
using System.Net.Http.Headers;
using System.Net.Http;
using PostOffice.API.DTOs.ParcelService;
using System.Text;
using PostOffice.Admin.Services;

namespace PostOffice.Admin.Services
{
    public class ParcelServiceAPIAdmin : IParcelServiceAPIAdmin
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParcelServiceAPIAdmin(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        
        public async Task<ApiResult<ParcelServiceBaseDTO>> GetById(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/ParcelService/GetById/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ParcelServiceBaseDTO>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ParcelServiceBaseDTO>>(body);
        }
       

        public async Task<ApiResult<bool>> UpdateParcelService(int id, ParcelServiceUpdateDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/ParcelService/Update/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }

    }
}
