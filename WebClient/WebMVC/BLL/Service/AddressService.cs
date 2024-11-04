using BLL.IService;
using BLL.Model;
using BLL.Model.AddressDtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace BLL.Service
{
    public class AddressService : IAddressService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public AddressService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<List<CityDtos>> GetListCity()
        {
            var url = _configuration["https:localAPI"] + "Address/Cities";
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listCity = JsonConvert.DeserializeObject<ApiResponse<List<CityDtos>>>(content);
            return listCity.Data;
        }

        public async Task<List<DistrictDtos>> ListDistrictByCity(int CityID)
        {
            var url = _configuration["https:localAPI"] + "Address/City/" + CityID + "/District";
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listDistricts = JsonConvert.DeserializeObject<ApiResponse<List<DistrictDtos>>>(content);
            return listDistricts.Data;
        }

        public async Task<List<WardDtos>> ListWardByDistrict(int DistrictID)
        {
            var url = _configuration["https:localAPI"] + "Address/District/" + DistrictID + "/Wards";
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var listWards = JsonConvert.DeserializeObject<ApiResponse<List<WardDtos>>>(content);
            return listWards.Data;
        }

        public async Task<CityDtos> GetCityByWard(int WardID)
        {
            var url = _configuration["https:localAPI"] + "Address/City/Ward/" + WardID;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var cityName = JsonConvert.DeserializeObject<ApiResponse<CityDtos>>(content);
            return cityName.Data;
        }
    }
}
