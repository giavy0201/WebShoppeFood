using BLL.IService;
using BLL.Model;
using BLL.Model.Food;
using BLL.Model.Menu;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class MenuService : IMenuService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public MenuService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        public async Task<List<MenuDtos>> ListMenuStore(int StoreID)
        {
            var url = _configuration["https:localAPI"] + "Menu/Store/System/" + StoreID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listmenu = JsonConvert.DeserializeObject<ApiResponse<List<MenuDtos>>>(content);
                return listmenu.Data;
            }
        }

        public async Task<int> TotleFoodInMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Food/System/Menu/" + MenuID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listfood = JsonConvert.DeserializeObject<ApiResponse<List<FoodDtos>>>(content);
                return listfood.Data.Select(x => x.Id).Count();
            }
        }

        public async Task<ApiResponse<ApiRequest>> UpdateStatusMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Menu/" + MenuID + "/Status";
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<ApiRequest>>(content);
            return request;
        }
        public async Task<ApiResponse<string>> InsertMenu(ReqCreateMenu modelMenu)
        {
            var url = _configuration["https:localAPI"] + "Menu";
            string data = JsonConvert.SerializeObject(modelMenu);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonResponse);
            return request;
        }

        public async Task<MenuDtos> DetailMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Menu/System/" + MenuID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var menu = JsonConvert.DeserializeObject<ApiResponse<MenuDtos>>(content);
                return menu.Data;
            }
        }

        public async Task<ApiResponse<string>> UpdateMenu(ReqUpdateMenu modelMenu)
        {
            var url = _configuration["https:localAPI"] + "Menu";
            string data = JsonConvert.SerializeObject(modelMenu);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(jsonResponse);
            return request;
        }

        public async Task<ApiResponse<string>> DeleteMenu(int MenuID)
        {
            var url = _configuration["https:localAPI"] + "Menu?MenuID=" + MenuID;
            var data = await _httpClient.DeleteAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var request = JsonConvert.DeserializeObject<ApiResponse<string>>(content);
            return request;
        }
    }
}
