using BLL.IService;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        private readonly IAddressService _addressService;
        public HomeController(IProductService productService, IStoreService storeService, IAddressService addressService)
        {
            _productService = productService;
            _storeService = storeService;
            _addressService = addressService;
        }

        /// <summary>
        /// Home Page
        /// </summary>
        /// <returns></returns>
        public IActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// list Preferential in Home Page
        /// </summary>
        /// <param name="reqStorePre"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListPreferentialHome(ReqStorePreferential reqStorePre)
        {
            try
            {
                var listStore = await _storeService.ListStorePreferential(reqStorePre);
                if (listStore.IsSuccess != false)
                {
                    return PartialView("_listPreferentialHome", listStore.Data);
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

        /// <summary>
        /// list content option in home page
        /// </summary>
        /// <param name="CateID"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListOptionHome(int CateID)
        {
            try
            {
                var listContent = await _productService.GetListContentByCate(CateID);
                return PartialView("_listOptionHome", listContent);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }

        }

        /// <summary>
        /// partial view list collection in home page
        /// </summary>
        /// <param name="reqCollectionHome"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListCollectionHome(ReqCollectionHome reqCollectionHome)
        {
            try
            {
                var listCollection = await _productService.ListCollection(reqCollectionHome);
                if (listCollection.IsSuccess != false)
                {
                    return PartialView("_listCollectionHome", listCollection.Data);
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

        /// <summary>
        /// list district option of menu bot in home page
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListDistrictMenuBot(int CityID)
        {
            try
            {
                var listDistrict = await _addressService.ListDistrictByCity(CityID);
                return PartialView("_listDistrictMenuBot", listDistrict);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }

        /// <summary>
        /// list Store Menu Bot in home page
        /// </summary>
        /// <param name="reqStoreMenuBot"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListStoreMenuBot(ReqStoreMenuBot reqStoreMenuBot)
        {
            try
            {
                var listStore = await _storeService.ListStoreByDistrict(reqStoreMenuBot);
                if (listStore.IsSuccess != false)
                {
                    return PartialView("_listStoreMenuBot", listStore.Data);
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

        /// <summary>
        /// page 404
        /// </summary>
        /// <returns></returns>
        [HttpGet("/NotFound")]
        public IActionResult PageNotFound()
        {
            return PartialView();
        }
    }
}