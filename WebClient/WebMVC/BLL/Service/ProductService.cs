using BLL.IService;
using BLL.Model;
using BLL.Model.ModelRequest;
using BLL.Model.ProductDtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class ProductService : IProductService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public ProductService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<List<CategoryDtos>> GetListCategory()
        {
            var url = _configuration["https:localAPI"] + "Products/Categories";
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listCate = JsonConvert.DeserializeObject<ApiResponse<List<CategoryDtos>>>(content);
            return listCate.Data;
        }

        public async Task<ApiResponse<List<CollectionDtos>>> ListCollection(ReqCollectionHome reqCollection)
        {
            var url = _configuration["https:localAPI"] + "Collections";
            string data = JsonConvert.SerializeObject(reqCollection);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var listCollection = JsonConvert.DeserializeObject<ApiResponse<List<CollectionDtos>>>(jsonResponse);
            return listCollection;
        }
        public async Task<CollectionDtos> DetailCollection(int CollectionID)
        {
            var url = _configuration["https:localAPI"] + "Collections/" + CollectionID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var detailCollection = JsonConvert.DeserializeObject<ApiResponse<CollectionDtos>>(content);
            return detailCollection.Data;
        }

        public async Task<List<ContentDtos>> GetListContentByCate(int CateID)
        {
            var url = _configuration["https:localAPI"] + "Products/Category/" + CateID + "/Contents";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listContent = JsonConvert.DeserializeObject<ApiResponse<List<ContentDtos>>>(content);
                return listContent.Data;
            }
        }
    }
}
