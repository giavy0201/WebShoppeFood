using BLL.IService;
using BLL.Model.Food;
using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol;
using WebSystemStore.Models;
using X.PagedList;

namespace WebSystemStore.Controllers
{

    public class FoodController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IMenuService _menuService;
        private readonly IFoodService _foodService;
        public FoodController(IHttpContextAccessor context, IMenuService menuService, IFoodService foodService)
        {
            _context = context;
            _menuService = menuService;
            _foodService = foodService;
        }

        public async Task<IActionResult> ListProduct(ModelSearchProduct db, int? page)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            if (page == null) page = 1;
            int pageSize = 5;
            int pageNum = page ?? 1;
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            db.StoreID = (int)StoreID;
            ViewBag.Keyword = db.Keyword;
            ViewBag.Menu = db.Menu.ToJson();
            ViewBag.MenuID = db.Menu;
            ViewBag.PriceFirst = db.PriceFirst;
            ViewBag.PriceEnd = db.PriceEnd;
            ViewBag.DiscountID = db.DiscountPrice;
            ViewBag.StatusID = db.StatusID;
            ViewBag.PriceID = db.SortPrice;
            var listfood = await _foodService.ListFoodSeach(db);
            return View(listfood.ToPagedList(pageNum, pageSize));
        }

        public async Task<IActionResult> ListFoodOfMenu(int MenuID)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            var listfood = await _foodService.ListFoodByMenu(MenuID);
            var menu = await _menuService.DetailMenu(MenuID);
            if (menu != null)
            {
                ViewBag.NameMenu = menu.Name;
            }
            ViewBag.MenuID = MenuID;
            if (listfood == null)
            {
                return View(ListEmpty.ListFood);
            }
            return View(listfood);
        }

        public async Task<FoodDtos> DetailFood(int FoodID)
        {
            var menu = await _foodService.DetailFood(FoodID);
            return menu;
        }

        public async Task<IActionResult> SetStatusproduct(int FoodID)
        {
            var request = await _foodService.UpdateStatusFood(FoodID);
            return Json(request);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateFood(ReqUpdateFood modelfood)
        {
            modelfood.AdminName = _context.HttpContext.Session.GetString("customer");
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            if (modelfood.formFile != null)
            {
                //Save image to wwwroot/image
                string extension = Path.GetExtension(modelfood.formFile.FileName);
                string fileName = Path.GetFileNameWithoutExtension(modelfood.formFile.FileName) + extension;
                string path = $"D:\\ShoppeFood\\ImageShoppeFood\\ListStore\\Store-{StoreID}\\" + fileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await modelfood.formFile.CopyToAsync(fileStream);
                }
                modelfood.Img = fileName;
            }
            var request = await _foodService.UpdateFood(modelfood);
            return Json(request);
        }

        [HttpPost]
        public async Task<IActionResult> CreateFood(ReqInsertFood modelfood)
        {
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            modelfood.AdminName = _context.HttpContext.Session.GetString("customer");
            modelfood.Status = 0;
            if (modelfood.formFile != null)
            {
                string extension = Path.GetExtension(modelfood.formFile.FileName);
                string fileName = Path.GetFileNameWithoutExtension(modelfood.formFile.FileName) + extension;
                string path = $"D:\\ShoppeFood\\ImageShoppeFood\\ListStore\\Store-{StoreID}\\" + fileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await modelfood.formFile.CopyToAsync(fileStream);
                }
                modelfood.Img = fileName;
            }
            var request = await _foodService.InsertFood(modelfood);
            return Json(request);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteFood(string FoodID)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            int id = int.Parse(FoodID);
            var request = await _foodService.DeleteFood(id);
            return Json(request);
        }
    }
}
