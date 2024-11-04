using BLL.IService;
using BLL.Models.DTOs;
using BLL.Models.DTOs.Cart;
using BLL.Models.Request;
using DAL.Entities;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]

    public class CartController : Controller
    {
        private readonly ICartService _cartService;

        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }

        //[HttpGet("monthly")]
        //public async Task<IActionResult> GetOrdersByMonth(int storeID, int month)
        //{
        //    try
        //    {
        //        // Lấy danh sách các đơn hàng từ service
        //        var orders = await _cartService.GetOrdersByStoreAndMonth(storeID, month);

        //        // Kiểm tra nếu danh sách đơn hàng không tồn tại
        //        if (orders == null || !orders.Any())
        //        {
        //            return BadRequest(ApiResponse<string>.BadRequest("Không có đơn hàng trong tháng này cho cửa hàng này"));
        //        }

        //        // Group các đơn hàng theo tháng và đếm số lượng đơn hàng trong mỗi tháng
        //        var ordersByMonth = orders.GroupBy(o => o.TimeOrder?.Month)
        //                                  .Where(g => g.Key.HasValue) // Lọc ra những nhóm có tháng không null
        //                                  .Select(g => new
        //                                  {
        //                                      Month = g.Key,
        //                                      OrderCount = g.Count()
        //                                  })
        //                                  .ToList();

        //        return Ok(ordersByMonth);
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
        //    }
        //}

        [HttpPost("/Carts")]
        public async Task<IActionResult> AddCart(CartRequest cartRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _cartService.InsertCart(cartRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Order Thất Bại"));
                }
                return Ok(ApiResponse<Cart>.Success("Order Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpGet("{CartID}")]
        public async Task<IActionResult> CartByID(int CartID)
        {
            var cart = await _cartService.CartByID(CartID);
            if (cart == null)
            {
                return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
            }
            return Ok(ApiResponse<CartSystem>.Success("Truy Xuất Thành Công", cart));
        }

        [HttpPost("Search")]
        public async Task<IActionResult> ListCartByStore(SearchCartRequest searchCartRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _cartService.ListCartByStore(searchCartRequest);
                if (!result.Any())
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Truy Xuất Thất Bại"));
                }
                return Ok(ApiResponse<List<CartSystem>>.Success("Truy Xuất Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
        [HttpPost("Status")]
        public async Task<IActionResult> UpdateCartOrder(SetStatusCartReq setStatusCartRequest)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    var validate = Validate.ValidateInput(ModelState);
                    return BadRequest(ApiResponse<string>.BadRequest(validate));
                }
                var result = await _cartService.UpdateCartOrder(setStatusCartRequest);
                if (result == null)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Cập Nhập Thất Bại"));
                }
                return Ok(ApiResponse<ModelApiRequest>.Success("Cập Nhập Thành Công", result));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }

        [HttpGet("/TotalMoneyForMonth")]
        public async Task<IActionResult> GetTotalMoneyForMonth(int storeID, int month, int year)
        {
            try
            {
                // Kiểm tra đầu vào
                if (storeID <= 0 || month < 1 || month > 12 || year < 1)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Thông tin không hợp lệ."));
                }

                // Gọi phương thức để tính tổng doanh thu trong một tháng
                var totalMoney = await _cartService.TotalMoneyForMonth(storeID, month, year);

                // Trả về kết quả thành công
                return Ok(ApiResponse<double>.Success("Tổng doanh thu trong tháng đã được truy xuất thành công.", totalMoney));
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có ngoại lệ xảy ra
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
        [HttpGet("yearly/{storeID}/{year}")]
        public async Task<IActionResult> GetYearlyRevenue(int storeID, int year)
        {
            try
            {
                // Kiểm tra đầu vào
                if (storeID <= 0 || year < 1)
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Thông tin không hợp lệ."));
                }

                // Gọi phương thức để tính tổng doanh thu trong năm
                var yearlyRevenue = await _cartService.TotalMoneyForYear(storeID, year);

                // Trả về kết quả thành công
                return Ok(ApiResponse<List<double>>.Success("Danh sách doanh thu theo năm đã được truy xuất thành công.", yearlyRevenue));
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có ngoại lệ xảy ra
                return StatusCode(500, ApiResponse<string>.Error($"Error: {ex.Message}"));
            }
        }
        [HttpGet("GetOrCreateCartId/{email}")]
        public async Task<IActionResult> GetCartByEmail(string email)
        {
            try
            {
                // Kiểm tra xem email có giá trị hợp lệ hay không
                if (string.IsNullOrEmpty(email))
                {
                    return BadRequest(ApiResponse<string>.BadRequest("Email không được để trống."));
                }

                // Gọi phương thức từ service để lấy danh sách ID của giỏ hàng dựa trên email
                var cartIds = await _cartService.GetCartIdByEmail(email);

                // Kiểm tra xem có giỏ hàng nào được tìm thấy hay không
                if (cartIds == null || !cartIds.Any())
                {
                    return NotFound(ApiResponse<string>.NotFound("Không tìm thấy giỏ hàng cho email đã cho."));
                }

                // Trả về danh sách ID của giỏ hàng thành công
                return Ok(ApiResponse<List<int>>.Success("Lấy ID giỏ hàng thành công", cartIds));
            }
            catch (Exception ex)
            {
                // Trả về lỗi nếu có ngoại lệ xảy ra
                return StatusCode(500, ApiResponse<string>.Error($"Lỗi: {ex.Message}"));
            }
        }



    }
}
