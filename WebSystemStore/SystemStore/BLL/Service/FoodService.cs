using BLL.IService;
using BLL.Model;
using BLL.Model.Food;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class FoodService : IFoodService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public FoodService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<FoodDtos> DetailFood(int FoodID)
        {
            var url = _configuration["https:localAPI"] + "Food/System/" + FoodID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var food = JsonConvert.DeserializeObject<ApiResponse<FoodDtos>>(content);
                return food.Data;
            }
        }

        public async Task<List<FoodDtos>> ListFoodByMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Food/System/Menu/" + MenuID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listfood = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(content);
                return listfood.Data;
            }
        }

        public async Task<ApiResponse<string>> UpdateFood(ReqUpdateFood modelfood)
        {
            var url = _configuration["https:localAPI"] + "Food";
            string data = JsonConvert.SerializeObject(modelfood);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonResponse);
            return request;
        }

        public async Task<ApiResponse<ApiRequest>> UpdateStatusFood(int FoodID)
        {
            var url = _configuration["https:localAPI"] + "Food/" + FoodID + "/Status";
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

        public async Task<ApiResponse<string>> InsertFood(ReqInsertFood modelfood)
        {
            var url = _configuration["https:localAPI"] + "Food";
            string data = JsonConvert.SerializeObject(modelfood);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonResponse);
            return request;
        }

        public async Task<ApiResponse<string>> DeleteFood(int FoodID)
        {
            var url = _configuration["https:localAPI"] + "Food?FoodID=" + FoodID;
            var data = await _httpClient.DeleteAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(content);
            return request;
        }

        public async Task<List<FoodDtos>> ListFoodByStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Food/Store/System/" + StoreID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listfood = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(content);
                return listfood.Data;
            }
        }

        public async Task<List<FoodDtos>> ListFoodSeach(ModelSearchProduct modelSearch)
        {
            var url = _configuration["https:localAPI"] + "Food/Search";
            string data = JsonConvert.SerializeObject(modelSearch);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsondata);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var request = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(jsonResponse);
                return request.Data;
            }
        }
    }
}
