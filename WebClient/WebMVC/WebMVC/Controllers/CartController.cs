
using BLL.IService;
using BLL.Model;
using BLL.Model.Cart;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Security.Claims;
using WebMVC.Helper;
using WebMVC.Models.Payments;

namespace WebMVC.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStoreService _storeService;
        private readonly ICartService _cartService;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CartController(IHttpContextAccessor context, IStoreService storeService, ICartService cartService, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _storeService = storeService;
            _cartService = cartService;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        //private async Task<List<int>> GetOrCreateCartIds()
        //{
        //    var customerId = _context.HttpContext.Session.GetString("customerID");
        //    return await _cartService.GetOrCreateCartIdByCustomerId(customerId);
        //}

        private async Task<List<CartModel>> GetListCart()
        {
            var listCart = _context.HttpContext.Session.Get<List<CartModel>>("Cart");

            if (listCart == null)
            {
                listCart = new List<CartModel>();
                _context.HttpContext.Session.Set("Cart", listCart);
            }
            return listCart;
        }

        private double TotalMoney()
        {
            var listCart = _context.HttpContext.Session.Get<List<CartModel>>("Cart");
            return listCart.Sum(n => n.TotalMoney);
        }

        public async Task<IActionResult> AddToCart(int FoodID)
        {
            var listCart = await GetListCart();
            var cart = listCart.FirstOrDefault(item => item.FoodID == FoodID);

            if (cart == null)
            {
                var food = await _storeService.DetailFood(FoodID);
                if (food == null)
                {
                    TempData["ErrorMessage"] = "Sản phẩm không tồn tại.";
                    return Redirect(Request.Headers["Referer"].ToString());
                }

                cart = new CartModel(FoodID, food.Name, food.PriceDiscount ?? 0, food.StoreID ?? 0);
                if (listCart.Any(x => x.StoreID != cart.StoreID))
                {
                    TempData["ErrorMessage"] = "Bạn không thể thêm sản phẩm từ cửa hàng khác vào giỏ hàng. Hãy xóa giỏ hàng hoặc đặt hàng.";
                    return Redirect(Request.Headers["Referer"].ToString());
                }
                listCart.Add(cart);
            }
            else
            {
                cart.FoodQuantity++;
            }

            _context.HttpContext.Session.Set("Cart", listCart);
            _context.HttpContext.Session.SetString("TotalMoney", string.Format("{0:0,0}", TotalMoney()));
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> ListCart()
        {
            var listCart = await GetListCart();
            return PartialView("_listcart", listCart);
        }

        public async Task<IActionResult> RemoveFoodCart(int FoodID, string strUrl)
        {
            var listCart = await GetListCart();
            var food = listCart.SingleOrDefault(x => x.FoodID == FoodID);

            if (food != null)
            {
                listCart.RemoveAll(x => x.FoodID == FoodID);
                if (listCart.Count == 0)
                {
                    _context.HttpContext.Session.Remove("Cart");
                }
                else
                {
                    _context.HttpContext.Session.Set("Cart", listCart);
                }
                _context.HttpContext.Session.SetString("TotalMoney", string.Format("{0:0,0}", TotalMoney()));
            }
            return Redirect(strUrl);
        }

        public async Task<IActionResult> UpdateCart(int FoodID, int FoodQuantity)
        {
            var listCart = await GetListCart();
            var food = listCart.FirstOrDefault(x => x.FoodID == FoodID);

            if (food != null)
            {
                food.FoodQuantity = FoodQuantity;
                _context.HttpContext.Session.Set("Cart", listCart);
                _context.HttpContext.Session.SetString("TotalMoney", string.Format("{0:0,0}", TotalMoney()));
            }
            return Json(new { success = true });
        }

        public IActionResult RemoveCart(string strUrl)
        {
            _context.HttpContext.Session.Remove("Cart");
            return Redirect(strUrl);
        }

        public async Task<IActionResult> ViewOrder()
        {
            var listCart = await GetListCart();
            return View(listCart);
        }

        [HttpGet("/Cart")]
        public async Task<IActionResult> Cart()
        {
            if (_context.HttpContext.Session.GetString("customer") == null)
            {
                return RedirectToAction("Index", "Home");
            }
            var listCart = await GetListCart();
            return View(listCart);
        }

        [HttpGet]
        public IActionResult DatHang()
        {
            if (_context.HttpContext.Session.GetString("customer") == null)
            {
                return RedirectToAction("Login", "Customer");
            }
            return RedirectToAction("Cart", "Cart");
        }

        [HttpPost]
        public async Task<IActionResult> DatHang([FromBody] ReqOrder reqOrder)
        {
            var listCart = await GetListCart();
            if (listCart.Count == 0)
            {
                return Json(new { StatusCode = 0, Message = "Giỏ hàng của bạn đang trống." });
            }

            reqOrder.StoreId = listCart.Select(x => x.StoreID).FirstOrDefault();
            reqOrder.CustomerId = _context.HttpContext.Session.GetString("customerID");
            reqOrder.Details = listCart.Select(food => new ReqDetailCart
            {
                FoodId = food.FoodID,
                Quantity = food.FoodQuantity,
                Price = food.FoodPrice
            }).ToList();

            var req = await _storeService.OrderFood(reqOrder);
            if (req.StatusCode == 1)
            {
                _context.HttpContext.Session.Remove("Cart");
            }
            return Json(req);
        }
        //public IActionResult GetCartDetailsByCustomerId()
        //{
        //    return View();
        //}
        public async Task<IActionResult> History()
        {
            var customerEmail = _context.HttpContext.Session.GetString("customerEmail");
            if (string.IsNullOrEmpty(customerEmail))
            {
                return RedirectToAction("Login", "Customer");
            }

            var orders = await _cartService.GetOrderHistoryByEmail(customerEmail);
            return View(orders);
        }


        public async Task<IActionResult> Vnpay(int typePaymentVN, int foodId, int storeId)
        {
            var vnpay = new VnPayLibrary();
            var vnp_Url = _configuration["vnp_Url"];
            var vnp_TmnCode = _configuration["vnp_TmnCode"];
            var vnp_HashSecret = _configuration["vnp_HashSecret"];
            var vnp_Returnurl = _configuration["vnp_Returnurl"];

            var food = await _storeService.DetailFood(foodId);
            if (food == null)
            {
                return NotFound();
            }

            var store = await _storeService.DetailStore(storeId);
            if (store == null)
            {
                return NotFound();
            }

            long amount = (long)(food.Price * 100);

            vnpay.AddRequestData("vnp_Version", VnPayLibrary.VERSION);
            vnpay.AddRequestData("vnp_Command", "pay");
            vnpay.AddRequestData("vnp_TmnCode", vnp_TmnCode);
            vnpay.AddRequestData("vnp_Amount", amount.ToString());
            vnpay.AddRequestData("vnp_CreateDate", DateTime.Now.ToString("yyyyMMddHHmmss"));
            vnpay.AddRequestData("vnp_CurrCode", "VND");
            vnpay.AddRequestData("vnp_IpAddr", Utils.GetIpAddress(_context.HttpContext));
            vnpay.AddRequestData("vnp_Locale", "vn");
            vnpay.AddRequestData("vnp_OrderInfo", $"Thanh toán món {food.Name} tại cửa hàng {store.Name}");
            vnpay.AddRequestData("vnp_OrderType", "other");
            vnpay.AddRequestData("vnp_ReturnUrl", vnp_Returnurl);
            vnpay.AddRequestData("vnp_TxnRef", Guid.NewGuid().ToString("N"));

            string paymentUrl = vnpay.CreateRequestUrl(vnp_Url, vnp_HashSecret);
            return Redirect(paymentUrl);
        }
    }
}

