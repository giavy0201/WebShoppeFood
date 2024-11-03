using BLL.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using WebSystemStore.Models;

namespace WebSystemStore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHttpContextAccessor _context;
        private readonly ICartService _cartService;
        public HomeController(ILogger<HomeController> logger, IHttpContextAccessor context, ICartService cartService)
        {
            _logger = logger;
            _context = context;
            _cartService = cartService;
        }

        public IActionResult Index()
        {
            if(_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }    
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> GetMonthlyChart(int month, int year)
        {
            try
            {
                // Lấy storeID từ session
                int? storeID = HttpContext.Session.GetInt32("storeID");

                // Kiểm tra nếu storeID không hợp lệ (null hoặc bằng 0) thì trả về BadRequest
                if (!storeID.HasValue || storeID == 0)
                {
                    return BadRequest("Vui lòng đăng nhập để xem biểu đồ.");
                }

                // Tạo URL để gửi yêu cầu API
                string apiUrl = $"https://localhost:7152/TotalMoneyForMonth?storeID={storeID}&month={month}&year={year}";

                // Gửi yêu cầu GET đến endpoint để lấy tổng doanh thu hàng tháng
                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(apiUrl);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var responseData = JsonConvert.DeserializeObject<dynamic>(content);

                        // Kiểm tra xem dữ liệu trả về có hợp lệ không
                        if (responseData.isSuccess)
                        {
                            double totalRevenue = responseData.data;
                            return Json(totalRevenue);
                        }
                        else
                        {
                            // Xử lý lỗi nếu dữ liệu trả về không hợp lệ
                            _logger.LogError($"Error getting monthly revenue data: {responseData.message}");
                            return BadRequest("An error occurred while retrieving monthly revenue data.");
                        }
                    }
                    else
                    {
                        // Xử lý lỗi nếu yêu cầu thất bại
                        _logger.LogError($"Error getting monthly revenue data from API. StatusCode: {response.StatusCode}");
                        return BadRequest("An error occurred while retrieving monthly revenue data.");
                    }
                }
            }
            catch (Exception ex)
            {
                // Xử lý nếu có lỗi và trả về lỗi 500
                _logger.LogError($"An error occurred while retrieving monthly revenue data: {ex.Message}");
                return StatusCode(500, "An error occurred while retrieving monthly revenue data.");
            }
        }







        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        

    }
}