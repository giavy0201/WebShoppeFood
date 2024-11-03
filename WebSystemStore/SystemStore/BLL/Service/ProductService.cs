using BLL.IService;
using BLL.Model;
using BLL.Model.Product;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

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
        public async Task<ContentDtos> GetContentByID(int ContentID)
        {
            var url = _configuration["https:localAPI"] + "Products/Content/" + ContentID;
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var contentproduct = JsonConvert.DeserializeObject<ApiResponse<ContentDtos>>(content);
                return contentproduct.Data;
            }
        }

        public async Task<List<ContentDtos>> ListContents()
        {
            var url = _configuration["https:localAPI"] + "Products/Contents";
            var data = await _httpClient.GetAsync(url);
            if (!data.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var content = await data.Content.ReadAsStringAsync();
                var listcontent = JsonConvert.DeserializeObject<ApiResponse<List<ContentDtos>>>(content);
                return listcontent.Data;
            }
        }
    }
}
