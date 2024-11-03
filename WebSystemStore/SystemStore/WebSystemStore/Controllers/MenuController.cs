using BLL.IService;
using BLL.Model.Menu;
using Microsoft.AspNetCore.Mvc;

namespace WebSystemStore.Controllers
{
    public class MenuController : Controller
    {
        private readonly IHttpContextAccessor _context;
        private readonly IMenuService _menuService;
        public MenuController(IHttpContextAccessor context, IMenuService menuService)
        {
            _context = context;
            _menuService = menuService;
        }
        public async Task<IActionResult> ListMenu()
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            var listmenu = await _menuService.ListMenuStore((int)StoreID);
            return View(listmenu);
        }

        public async Task<IActionResult> SetStatusMenu(int MenuID)
        {
            await _menuService.UpdateStatusMenu(MenuID);
            return Redirect(Request.Headers["Referer"].ToString());
        }
        [HttpPost]
        public async Task<IActionResult> CreateMenu([FromBody] ReqCreateMenu modelmenu)
        {
            var StoreID = _context.HttpContext.Session.GetInt32("storeID");
            modelmenu.StoreID = (int)StoreID;
            modelmenu.AdminName = _context.HttpContext.Session.GetString("customer");
            modelmenu.Status = 0;
            var request = await _menuService.InsertMenu(modelmenu);
            return Json(request);
        }

        public async Task<MenuDtos> DetailMenu(int MenuID)
        {
            var menu = await _menuService.DetailMenu(MenuID);
            return menu;
        }

        [HttpPut]
        public async Task<IActionResult> UpdateMenu([FromBody] ReqUpdateMenu modelmenu)
        {
            modelmenu.AdminName = _context.HttpContext.Session.GetString("customer");
            var request = await _menuService.UpdateMenu(modelmenu);
            return Json(request);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteMenu(string MenuID)
        {
            if (_context.HttpContext.Session.GetInt32("storeID") == null)
            {
                return RedirectToAction("Login", "Store");
            }
            int id = int.Parse(MenuID);
            var request = await _menuService.DeleteMenu(id);
            return Json(request);
        }
    }
}
