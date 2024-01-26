using Newtonsoft.Json;
using PostOffice.API.DTOs.Common;
using PostOffice.API.DTOs.User;
using System.Net.Http.Headers;
using System.Net.Http;
using PostOffice.API.DTOs.ParcelOrder;
using System.Text;
using PostOffice.API.DTOs.TrackHistory;

namespace PostOffice.Admin.Services
{
    public class ParcelOrderApiClient : IParcelOrderApiClient
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ParcelOrderApiClient(IHttpClientFactory httpClientFactory,
                   IHttpContextAccessor httpContextAccessor,
                    IConfiguration configuration)
        {
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<ApiResult<PagedResult<ParcelOrderViewDTO>>> GetAllParcelOrderPaging(GetUserPagingRequest request)
        {
            var client = _httpClientFactory.CreateClient();
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/ParcelOrder/GetAll/paging?pageIndex=" +
                $"{request.PageIndex}&pageSize={request.PageSize}&keyword={request.Keyword}");
            var body = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<ApiSuccessResult<PagedResult<ParcelOrderViewDTO>>>(body);
            return orders;
        }

        public async Task<ApiResult<ParcelOrderViewDTO>> GetById(int id)
        {
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);
            var response = await client.GetAsync($"/api/ParcelOrder/GetById/{id}");
            var body = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<ParcelOrderViewDTO>>(body);

            return JsonConvert.DeserializeObject<ApiErrorResult<ParcelOrderViewDTO>>(body);
        }

		public Task<ApiResult<TrackHistoryViewDTO>> GetHistoryByOrderId(int id)
		{
			throw new NotImplementedException();
		}

		public async Task<ApiResult<bool>> UpdateParcelOrder(int id, ParcelOrderUpdateDTO request)
        {
            var client = _httpClientFactory.CreateClient();
            client.BaseAddress = new Uri(_configuration["BaseAddress"]);
            var sessions = _httpContextAccessor.HttpContext.Session.GetString("Token");

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", sessions);

            var json = JsonConvert.SerializeObject(request);
            var httpContent = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await client.PutAsync($"/api/ParcelOrder/Update/{id}", httpContent);
            var result = await response.Content.ReadAsStringAsync();
            if (response.IsSuccessStatusCode)
                return JsonConvert.DeserializeObject<ApiSuccessResult<bool>>(result);

            return JsonConvert.DeserializeObject<ApiErrorResult<bool>>(result);
        }
       
    }
}
