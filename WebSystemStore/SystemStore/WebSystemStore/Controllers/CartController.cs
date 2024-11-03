using BLL.IService;
using BLL.Model.Cart;
using Microsoft.AspNetCore.Mvc;
using WebSystemStore.Models;
using X.PagedList;

namespace WebSystemStore.Controllers
{
    public class CartController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStoreService _storeService;
        public CartController(IHttpContextAccessor context, IStoreService storeService)
        {
            _context = context;
            _storeService = storeService;
        }
        public async Task<IActionResult> ListOrderNow(ModelSelectCart req)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            var listcart = await _storeService.ListCartOrderTodayByStore(req);
            if (listcart.Data == null)
            {
                return View(ListEmpty.ListCart);
            }
            return View(listcart.Data);
        }

        public async Task<IActionResult> SetStatusOrder(SetStatusCart setReq)
        {
            await _storeService.UpdateStatusOrder(setReq);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<IActionResult> DetailCartByID(int CartID)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            ViewBag.CodeCart = CartID;
            var cart = await _storeService.SelectCartByID(CartID);
            var totalMoney = cart.DetailCarts.Select(x=>x.TotalMoney).Sum();
            ViewBag.TotalMoney = totalMoney;
            return View(cart.DetailCarts);
        }

        public async Task<IActionResult> ListCart(ModelSelectCart model, int? page)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            if (page == null) page = 1;
            int pageSize = 10;
            int pageNum = page ?? 1;
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            model.StoreID = (int)StoreID;
            ViewBag.Status = model.StatusID;
            ViewBag.DayStart = model.DayStart;
            ViewBag.DayEnd = model.DayEnd;
            ViewBag.CartID = model.CartID;
            ViewBag.Delivery = model.Delivery;
            ViewBag.Phone = model.Phone;
            var listcart = await _storeService.ListCartByStore(model);
            return View(listcart.ToPagedList(pageNum, pageSize));
        }

        public async Task<List<object>> GetTotalCart()
        {
            var model = new ModelSelectCart();
            var storeID = _context.HttpContext.Session.GetInt32("storeID");
            model.StoreID = (int)storeID;
            var listcart = await _storeService.ListCartByStore(model);

            List<object> data = new List<object>();
            var listCount = new List<int>();
            DateTime startDate = DateTime.Now.Date.AddDays(-6);

            List<DateTime> listDay = listcart.Where(x => x.TimeOrder >= startDate).Select(y => y.TimeOrder.Date).Distinct().ToList();

            foreach (var date in listDay)
            {
                int totalCart = listcart.Count(x => x.TimeOrder.Date == date);
                listCount.Add(totalCart);
            }
            data.Add(listDay);
            data.Add(listCount);

            return data;
        }
    }
}
