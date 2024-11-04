using BLL.IService;
using BLL.Model.Customer;
using BLL.Model.ModelRequest;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System.Text;

namespace BLL.Service
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        public CustomerService(IConfiguration configuration)
        {
            _httpClient = new HttpClient();
            _configuration = configuration;
        }
        public async Task<int> CheckGmail(string email)
        {
            var url = _configuration["https:localAPI"] + "Customers/Check-gmail/" + email;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var checkgmail = JsonConvert.DeserializeObject<int>(content);
            return checkgmail;
        }

        public async Task<(int, string)> Register(ModelRegister dbRegister)
        {
            var url = _configuration["https:localAPI"] + "Customers/SignUp";
            string data = JsonConvert.SerializeObject(dbRegister);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync(url, jsondata);

            if (!response.IsSuccessStatusCode)
            {
                // Nếu yêu cầu không thành công, trả về mã lỗi và thông báo lỗi
                return ((int)response.StatusCode, "Đăng ký không thành công. Vui lòng thử lại.");
            }
            else
            {
                // Nếu yêu cầu thành công, đọc và phân tích dữ liệu JSON để nhận thông tin về người dùng đã đăng ký
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<CustomerDtos>(jsonResponse);

                // Trả về mã lỗi 1 (đăng ký thành công) và tên người dùng đã đăng ký
                return (1, customer.Username);
            }
        }
        public async Task<CustomerDtos> SignUp(ModelLogin dbLogin)
        {
            var url = _configuration["https:localAPI"] + "Customers/SignIn";
            string data = JsonConvert.SerializeObject(dbLogin);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsondata);
            if (!response.IsSuccessStatusCode)
            {
                return null;
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var customer = JsonConvert.DeserializeObject<CustomerDtos>(jsonResponse);
                return customer;
            }
        }

        public async Task<int> ForgotPassword(ReqForgotPassword reqForgotPassword)
        {
            var url = _configuration["https:localAPI"] + "Customers/ForgotPassword/ChangePassword";

            string data = JsonConvert.SerializeObject(reqForgotPassword);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync(url, jsondata);

            if (!response.IsSuccessStatusCode)
            {
                return 0;
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var request = JsonConvert.DeserializeObject<int>(jsonResponse);
                return request;
            }
        }

        public async Task<int> SendCodeForgotToEmail(string Email)
        {
            var url = _configuration["https:localAPI"] + "Customers/ForgotPassword/" + Email;
            var data = await _httpClient.GetAsync(url);
            var content = await data.Content.ReadAsStringAsync();
            var codeForgot = JsonConvert.DeserializeObject<int>(content);
            return codeForgot;
        }

        public async Task<(int, string)> UpdateCustomer(UpdateCustomerRequest updateRequest, string Email)
        {
            var url = _configuration["https:localAPI"] + "Customers/" + Email;
            string data = JsonConvert.SerializeObject(updateRequest);
            var jsondata = new StringContent(data, Encoding.UTF8, "application/json");

            var response = await _httpClient.PutAsync(url, jsondata);

            if (!response.IsSuccessStatusCode)
            {
                return ((int)response.StatusCode, "Update failed. Please try again.");
            }
            else
            {
                var jsonResponse = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<(int, string)>(jsonResponse);
                return result;
            }
        }

        public async Task<UpdateCustomerRequest> GetCustomerByEmailAsync(string email)
        {
            var url = _configuration["https:localAPI"] + "Customers/GetCustomerByEmail/" + email;
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                // Xử lý lỗi ở đây nếu cần thiết
                return null;
            }

            var jsonResponse = await response.Content.ReadAsStringAsync();
            var customer = JsonConvert.DeserializeObject<UpdateCustomerRequest>(jsonResponse);
            return customer;
        }
    }
}
