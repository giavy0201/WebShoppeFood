using BLL.IService;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class SearchController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly IHttpContextAccessor _context;

        public SearchController(IProductService productService, IStoreService storeService, IHttpContextAccessor context)
        {
            _productService = productService;
            _storeService = storeService;
            _context = context;
        }

        /// <summary>
        /// UI Search Page 
        /// </summary>
        /// <param name="keyword"></param>
        /// <returns></returns>
        public async Task<ActionResult> SearchPage(string? keyword)
        {
            if (keyword != null)
            {
                _context.HttpContext.Session.SetString("keyword", keyword);
            }
            return View();
        }

        /// <summary>
        /// remove keyword search
        /// </summary>
        /// <returns></returns>
        public async Task<ActionResult> removeKeyword()
        {
            _context.HttpContext.Session.Remove("keyword");
            return RedirectToAction("SearchPage", "Search");
        }

        /// <summary>
        /// list content option in search page
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListContentSearch(int CateID)
        {
            try
            {
                var ListContent = await _productService.GetListContentByCate(CateID);
                return PartialView("_listContentSearch", ListContent);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// partialview list store in search page
        /// </summary>
        /// <param name="reqSearch"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListStoreSearch(ReqSearch reqSearch)
        {
            try
            {
                var liststore = await _storeService.ListStoreSearch(reqSearch);
                if(liststore.IsSuccess != false)
                {
                    return PartialView("_listStoreSearch", liststore.Data);
                }
                else
                {
                    return PartialView("~/Views/Shared/_dataEmpty.cshtml");
                }
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }

        }
    }
}
