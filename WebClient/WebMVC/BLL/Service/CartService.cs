using BLL.IService;
using BLL.Model;
using BLL.Model.Cart;

using BLL.Model.Customer;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using BLL.Model.ModelStoreDtos;

namespace BLL.Service
{
    public class CartService : ICartService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
       



        public CartService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;

            
        }

        public async Task<List<int>> GetOrCreateCartIdByEmail(string email)
        {
            var url = _configuration["https:localAPI"] + "Cart/GetOrCreateCartId/" + email;
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Could not retrieve or create cart IDs");
            }

            var content = await response.Content.ReadAsStringAsync();
            var responseObject = JsonConvert.DeserializeObject<dynamic>(content);

            if (responseObject.isSuccess == false)
            {
                throw new Exception("Failed to get cart IDs from the response: " + responseObject.message);
            }

            var data = responseObject.data;
            var cartIds = new List<int>();

            foreach (var id in data)
            {
                cartIds.Add((int)id);
            }

            return cartIds;
        }

        public async Task<CartDtos> GetOrderHistory(int CartId)
        {
            var url = _configuration["https:localAPI"] + "Cart/" + CartId;
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Xử lý trường hợp không thành công
                return null;
            }

            var content = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<ApiResponse<CartDtos>>(content);
            return order.Data;
        }
        public async Task<List<CartDtos>> GetOrderHistoryByEmail(string email)
        {
            try
            {
                var cartIds = await GetOrCreateCartIdByEmail(email);

                var orders = new List<CartDtos>();
                foreach (var cartId in cartIds)
                {
                    var order = await GetOrderHistory(cartId);
                    if (order != null)
                    {
                        orders.Add(order);
                    }
                }
                return orders;
            }
            catch (Exception ex)
            {
                // Log the exception (ex)
                // Optionally, handle the error gracefully
                return new List<CartDtos>(); // Return an empty list or handle as needed
            }

        }
        

    }
}
