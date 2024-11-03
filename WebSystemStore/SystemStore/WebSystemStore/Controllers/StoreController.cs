using BLL.IService;
using BLL.Model.Address;
using BLL.Model.Store;
using Microsoft.AspNetCore.Mvc;

namespace WebSystemStore.Controllers
{
    public class StoreController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IStoreService _storeService;
        public StoreController(IHttpContextAccessor context, IStoreService storeService)
        {
            _context = context;
            _storeService = storeService;
        }
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] ReqLogin db)
        {
            var request = await _storeService.LoginSystemStore(db);
            if (request.IsSuccess == false)
            {
                return Json(request);
            }
            else
            {
                _context.HttpContext.Session.SetString("customer", request.Data.Username);
                _context.HttpContext.Session.SetInt32("storeID", request.Data.StoreID);
                return Json(request);
            }
        }

        public ActionResult Logout()
        {
            _context.HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> InfoStore()
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            var storeID = _context.HttpContext.Session.GetInt32("storeID");
            var store = await _storeService.DetailStore((int)storeID);
            return View(store);
        }

        public async Task<StoreDtos> DetailStore(int StoreID)
        {
            var store = await _storeService.DetailStore(StoreID);
            return store;
        }

        public async Task<List<DistrictDtos>> ListDistrictByCity(int CityID)
        {
            var districts = await _storeService.GetListDistrictByCity(CityID);
            return districts;
        }
        public async Task<List<WardDtos>> ListWardByDistrict(int DistrictID)
        {
            var wards = await _storeService.GetListWardByDistrict(DistrictID);
            return wards;
        }

        [HttpPost]
        public async Task<IActionResult> UpdateStore(ReqUpdateStore updateStore)
        {
            updateStore.AdminName = _context.HttpContext.Session.GetString("customer");
            var storeID = _context.HttpContext.Session.GetInt32("storeID");
            if (updateStore.formFile != null)
            {
                //Save image to wwwroot/image
                string extension = Path.GetExtension(updateStore.formFile.FileName);
                string fileName = Path.GetFileNameWithoutExtension(updateStore.formFile.FileName) + extension;
                string path = $"D:\\ShoppeFood\\ImageShoppeFood\\ListStore\\Store-{storeID}\\" + fileName;
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await updateStore.formFile.CopyToAsync(fileStream);
                }
                updateStore.Img = fileName;
            }
            var request = await _storeService.UpdateInfoStore(updateStore);
            return Json(request);
        }

        public async Task<IActionResult> SetStatusStore(int StoreID)
        {
            await _storeService.UpdateStatusStore(StoreID);
            return Redirect(Request.Headers["Referer"].ToString());
        }

        public async Task<ViewIDLocation> ListLocationID(int WardID)
        {
            var listID = await _storeService.LocationIDByWard(WardID);
            return listID;
        }
    }
}
