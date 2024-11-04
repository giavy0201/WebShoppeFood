using BLL.IService;
using BLL.Model;
using BLL.Model.Cart;
using BLL.Model.ModelRequest;
using BLL.Model.ModelStoreDtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class StoreService : IStoreService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public StoreService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<StoreDtos> DetailStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Stores/" + StoreID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var store = JsonConvert.DeserializeObject<ApiResponse<StoreDtos>>(content);
                return store.Data;
            }
        }

        public async Task<List<FoodDtos>> ListFoodOfMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Food/Menu/" + MenuID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listFoods = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(content);
            if (listFoods.IsSuccess == false)
            {
                var ListNull = new List<FoodDtos>();
                return ListNull;
            }
            return listFoods.Data;
        }

        public async Task<List<ListMenuDtos>> ListMenuStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Menu/Store/" + StoreID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listMenu = JsonConvert.DeserializeObject<ApiResponse<List<ListMenuDtos>>>(content);
            return listMenu.Data;
        }

        public async Task<ApiResponse<List<StoreDtos>>> ListStoreByDistrict(ReqStoreMenuBot reqStoreMenuBot)
        {
            var url = _configuration["https:localAPI"] + "Stores/District";
            string data = JsonConvert.SerializeObject(reqStoreMenuBot);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var liststore = JsonConvert.DeserializeObject<ApiResponse<List<StoreDtos>>>(jsonResponse);
            return liststore;
        }

        public async Task<ApiResponse<List<StoreDtos>>> ListStorePreferential(ReqStorePreferential reqStorePre)
        {
            var url = _configuration["https:localAPI"] + "Stores/Preferential";
            string data = JsonConvert.SerializeObject(reqStorePre);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var liststore = JsonConvert.DeserializeObject<ApiResponse<List<StoreDtos>>>(jsonResponse);
            return liststore;
        }

        public async Task<ApiResponse<List<StoreDtos>>> ListStoreSearch(ReqSearch reqSearch)
        {
            var url = _configuration["https:localAPI"] + "Stores/Search";
            string data = JsonConvert.SerializeObject(reqSearch);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var liststore = JsonConvert.DeserializeObject<ApiResponse<List<StoreDtos>>>(jsonResponse);
            return liststore;
        }

        public async Task<ApiResponse<List<ListStoreOfCollecDtos>>> StoreByCollection(ReqListStoreOfCollec reqStoreCollection)
        {
            var url = _configuration["https:localAPI"] + "Collections/Stores";
            string data = JsonConvert.SerializeObject(reqStoreCollection);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var liststore = JsonConvert.DeserializeObject<ApiResponse<List<ListStoreOfCollecDtos>>>(jsonResponse);
            return liststore;
        }

        public async Task<FoodDtos> DetailFood(int FoodID)
        {
            var url = _configuration["https:localAPI"] + "Food/" + FoodID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var food = JsonConvert.DeserializeObject<ApiResponse<FoodDtos>>(content);
            return food.Data;
        }

        public async Task<List<FoodDtos>> ListFoodOfStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Food/Store/ " + StoreID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var food = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(content);
            return food.Data;
        }

        public async Task<ReqOrderMess> OrderFood(ReqOrder reqOrder)
        {
            var url = _configuration["https:localAPI"] + "Carts";
            var req = new ReqOrderMess();
            string data = JsonConvert.SerializeObject(reqOrder);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);

            if (!response.IsSuccessStatusCode)
            {
                req.StatusCode = 0;
                req.StatusMessage = "Order Lỗi";
                return req;
            }
            else
            {
                req.StatusCode = 1;
                req.StatusMessage = "Order Thành Công";
                return req;
            }
        }

        public async Task<StoreDtos> GetOrderByFoodAndStoreId(int foodId, int storeId, string orderCode)
        {
            var url = _configuration["https:localAPI"] + $"Orders/FoodAndStore/{foodId}/{storeId}/{orderCode}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<StoreDtos>(content);
            return order;
        }
        public async Task<List<int>> GetFoodIdsByStore(int storeId)
        {
            var foods = await ListFoodOfStore(storeId);
            return foods.Select(food => food.Id).ToList(); // Assuming FoodDtos has an Id property
        }
        
    }
}
