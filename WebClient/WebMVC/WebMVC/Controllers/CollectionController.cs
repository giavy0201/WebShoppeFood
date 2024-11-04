using BLL.IService;
using BLL.Model.ModelRequest;
using Microsoft.AspNetCore.Mvc;

namespace WebMVC.Controllers
{
    public class CollectionController : Controller
    {
        private readonly IProductService _productService;
        private readonly IStoreService _storeService;
        public CollectionController(IProductService productService, IStoreService storeService)
        {
            _productService = productService;
            _storeService = storeService;
        }

        /// <summary>
        /// detail collection by ID
        /// </summary>
        /// <param name="CollectionID"></param>
        /// <returns></returns>
        [HttpGet("Collection/{CollectionID}/Stores")]
        public async Task<IActionResult> DetailCollection(int CollectionID)
        {
            try
            {
                var collection = await _productService.DetailCollection(CollectionID);
                return View(collection);
            }
            catch
            {
                return PartialView("~/Views/Shared/Error.cshtml");
            }
        }
        /// <summary>
        /// partial view list store in collection
        /// </summary>
        /// <param name="reqListStore"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult> ListStoreInCollection(ReqListStoreOfCollec reqListStore)
        {
            try
            {
                var listStore = await _storeService.StoreByCollection(reqListStore);
                if (listStore.IsSuccess != false)
                {
                    return PartialView("_listStoreInCollection", listStore.Data);
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
        /// view page all collection
        /// </summary>
        /// <returns></returns>
        [HttpGet("Colletions")]
        public IActionResult ListCollectionPage()
        {
            return View();
        }
        
        public async Task<ActionResult> ListCollectionInPage(ReqCollectionHome reqCollectionHome)
        {
            try
            {
                var listCollection = await _productService.ListCollection(reqCollectionHome);
                if (listCollection.IsSuccess != false)
                {
                    return PartialView("_listCollectionInPage", listCollection.Data);

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
