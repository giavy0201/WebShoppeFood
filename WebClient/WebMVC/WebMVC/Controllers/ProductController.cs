using BLL.IService;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly IStoreService _storeService;
        private readonly IAddressService _addressService;
        public ProductController(IStoreService storeService, IAddressService addressService)
        {
            _storeService = storeService;
            _addressService = addressService;
        }

        /// <summary>
        /// UI Preferential Page
        /// </summary>
        /// <returns></returns>
        [HttpGet("Preferential/Stores")]
        public IActionResult PreferentialPage()
        {
            return View();

        }

        /// <summary>
        /// Partial view list store in preferential page
        /// </summary>
        /// <param name="reqStorePre"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListStorePreferentialPage(ReqStorePreferential reqStorePre)
        {
            try
            {
                var listStore = await _storeService.ListStorePreferential(reqStorePre);
                if(listStore.IsSuccess != false)
                {
                    return PartialView("_listStorePreferentialPage", listStore.Data);
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
        /// partial view list district option in preferential page
        /// </summary>
        /// <param name="CityID"></param>
        /// <returns></returns>
        public async Task<ActionResult> ListDistrictPre(int CityID)
        {
            try
            {
                var listDistrict = await _addressService.ListDistrictByCity(CityID);
                return PartialView("_listDistrictPre", listDistrict);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }
    }
}
