using BLL.IService;
using BLL.Model;
using BLL.Model.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace BLL.Service
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<CartService> _logger;

        public CartService(IConfiguration configuration, ILogger<CartService> logger)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
            _logger = logger;
        }
        public async Task<CartDtos> GetCartByID(int CartID)
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
                var cart = JsonConvert.DeserializeObject<ApiResponse<CartDtos>>(content);
                return cart.Data;
            }
        }

        public async Task<double> GetRevenueForMonth(int storeID, int month, int year)
        {
            try
            {
                // Validate input parameters
                if (storeID <= 0 || month < 1 || month > 12 || year < 1)
                {
                    throw new ArgumentException("Invalid input parameters.");
                }

                // Call the API endpoint to get the total revenue for the specified month, year, and store
                var url = $"{_configuration["https:localAPI"]}TotalRevenueForMonth?storeID={storeID}&month={month}&year={year}";
                var response = await _httpClient.GetAsync(url);

                // Check if the request was successful
                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    double revenue = JsonConvert.DeserializeObject<double>(content);
                    return revenue;
                }
                else
                {
                    // Log an error if the request failed
                    _logger.LogError($"Error getting revenue for store {storeID}, month {month}, year {year}. StatusCode: {response.StatusCode}");
                    return -1; // Or any appropriate default value
                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that occur
                _logger.LogError($"Exception in GetRevenueForMonth: {ex.Message}");
                throw;
            }
        }



    }

}
