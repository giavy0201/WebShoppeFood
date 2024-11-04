using BLL.IService;
using BLL.Model;
using BLL.Model.Comment;
using BLL.Model.ModelRequest;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Service
{
    public class CommentService : ICommentService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;

        public CommentService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }

        public async Task<ApiResponse<CommentDtos>> AddComment(AddCommentRequest commentRequest)
        {
            var url = _configuration["https:localAPI"] + "Comment";
            string json = JsonConvert.SerializeObject(commentRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<CommentDtos>>(jsonResponse);

            return result;
        }

        

        public async Task<List<CommentDtos>> GetListCommentsByFoodId(int foodId)
        {
            var url = _configuration["https:localAPI"] + $"Comment/Food/{foodId}";
            var response = await _httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<List<CommentDtos>>>(jsonResponse);

            return result?.Data;
        }

        public async Task<List<CommentDtos>> GetListCommentsByStoreId(int storeId)
        {
            var url = _configuration["https:localAPI"] + "Comment/store/" + storeId;
            var response = await _httpClient.GetAsync(url);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            Console.WriteLine($"Response from API: {jsonResponse}");
            var result = JsonConvert.DeserializeObject<ApiResponse<List<CommentDtos>>>(jsonResponse);

            return result?.Data;
        }

        public async Task<ApiResponse<CommentDtos>> UpdateComment(int commentId, UpdateCommentRequest updateCommentRequest)
        {
            var url = _configuration["https:localAPI"] + "Comment/" + commentId;
            string json = JsonConvert.SerializeObject(updateCommentRequest);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, content);
            var jsonResponse = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ApiResponse<CommentDtos>>(jsonResponse);

            return result;
        }

        public async Task<bool> DeleteComment(int commentId)
        {
            var url = _configuration["https:localAPI"] + $"Comment/{commentId}";
            var response = await _httpClient.DeleteAsync(url);
            return response.IsSuccessStatusCode;
        }
    }
}
