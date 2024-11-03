using BLL.IService;
using BLL.Model;
using BLL.Model.Address;
using BLL.Model.Cart;
using BLL.Model.Store;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class StoreService : IStoreService
    {
        private readonly IHttpContextAccessor _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public StoreService(IConfiguration configuration, IHttpContextAccessor context)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _context = context;
        }
        public async Task<ApiResponse<ViewLogin>> LoginSystemStore(ReqLogin modelLogin)
        {
            var url = _configuration["https:localAPI"] + "Stores/Login";
            string data = JsonConvert.SerializeObject(modelLogin);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var liststore = JsonConvert.DeserializeObject<ApiResponse<ViewLogin>>(jsonResponse);
            return liststore;
        }

        public async Task<StoreDtos> DetailStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Stores/System/" + StoreID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var store = JsonConvert.DeserializeObject<ApiResponse<StoreDtos>>(content);
            return store.Data;
        }

        public async Task<List<CityDtos>> GetListCity()
        {
            var url = _configuration["https:localAPI"] + "Address/Cities";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listcity = JsonConvert.DeserializeObject<ApiResponse<List<CityDtos>>>(content);
                return listcity.Data;
            }
        }

        public async Task<List<DistrictDtos>> GetListDistrictByCity(int CityID)
        {
            var url = _configuration["https:localAPI"] + "Address/City/" + CityID + "/District";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listdistrict = JsonConvert.DeserializeObject<ApiResponse<List<DistrictDtos>>>(content);
                return listdistrict.Data;
            }
        }

        public async Task<List<WardDtos>> GetListWardByDistrict(int DistrictID)
        {
            var url = _configuration["https:localAPI"] + "Address/District/" + DistrictID + "/Wards";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listward = JsonConvert.DeserializeObject<ApiResponse<List<WardDtos>>>(content);
                return listward.Data;
            }
        }

        public async Task<ApiResponse<string>> UpdateInfoStore(ReqUpdateStore modelstore)
        {
            var url = _configuration["https:localAPI"] + "Stores";
            string data = JsonConvert.SerializeObject(modelstore);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonResponse);
            return request;
        }

        public async Task<ApiResponse<ApiRequest>> UpdateStatusStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Stores/" + StoreID + "/Status";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var request = JsonConvert.DeserializeObject<ApiResponse<ApiRequest>>(content);
                return request;
            }
        }

        public async Task<ViewIDLocation> LocationIDByWard(int WardID)
        {
            var url = _configuration["https:localAPI"] + "Address/Location/Ward/" + WardID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var location = JsonConvert.DeserializeObject<ApiResponse<ViewIDLocation>>(content);
                return location.Data;
            }
        }

        public async Task<ApiResponse<List<CartDtos>>> ListCartOrderTodayByStore(ModelSelectCart req)
        {
            var storeID = _context.HttpContext.Session.GetInt32("storeID");
            req.StoreID = (int)storeID;
            req.DayStart = DateTime.Today;
            var url = _configuration["https:localAPI"] + "Cart/Search";
            string data = JsonConvert.SerializeObject(req);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var listCart = JsonConvert.DeserializeObject<ApiResponse<List<CartDtos>>>(jsonResponse);
            return listCart;
        }

        public async Task<CartDtos> SelectCartByID(int CartID)
        {
            var url = _configuration["https:localAPI"] + "Cart/" + CartID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var detailcart = JsonConvert.DeserializeObject<ApiResponse<CartDtos>>(content);
                return detailcart.Data;
            }
        }

        public async Task<double> TotalMoneyToday(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Stores/" + StoreID + "/Today/TotalMoney";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var toltalmoney = JsonConvert.DeserializeObject<double>(content);
                return toltalmoney;
            }
        }

        public async Task<ApiRequest> UpdateStatusOrder(SetStatusCart setReq)
        {
            var req = new ApiRequest();
            var url = _configuration["https:localAPI"] + "Cart/Status";
            string data = JsonConvert.SerializeObject(setReq);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            if (!response.IsSuccessStatusCode)
            {
                return req;
            }
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                var request = JsonConvert.DeserializeObject<ApiResponse<ApiRequest>>(content);
                return request.Data;
            }
        }

        public async Task<List<CartDtos>> ListCartByStore(ModelSelectCart model)
        {
            var url = _configuration["https:localAPI"] + "Cart/Search";
            string data = JsonConvert.SerializeObject(model);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var listCart = JsonConvert.DeserializeObject<ApiResponse<List<CartDtos>>>(jsonResponse);
                return listCart.Data;
            }
        }

        public object GetMonthlyOrders()
        {
            throw new NotImplementedException();
        }
    }
}
